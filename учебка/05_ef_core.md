# Entity Framework Core — работа с базой данных

---

## Что такое EF Core

**EF Core** (Entity Framework Core) — это ORM (Object-Relational Mapper).  
Он позволяет работать с базой данных как с обычными C# объектами, без написания SQL.

```
БД таблица  products  →  C# класс  Product
БД строка             →  C# объект product
SELECT * FROM products →  db.Products.ToList()
INSERT INTO products   →  db.Products.Add(product); db.SaveChanges();
UPDATE products SET    →  product.Price = 999; db.SaveChanges();
DELETE FROM products   →  db.Products.Remove(product); db.SaveChanges();
```

---

## Шаг 1 — Установить NuGet пакеты

В проекте нужны три пакета (уже установлены в ShoeShop):

```
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Tools
Pomelo.EntityFrameworkCore.MySql
```

Установить через Package Manager Console:
```
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Pomelo.EntityFrameworkCore.MySql
```

---

## Шаг 2 — Scaffold (генерация кода из БД)

Вместо того чтобы писать классы вручную — EF Core генерирует их из существующей БД.

В Package Manager Console (Tools → NuGet Package Manager → Package Manager Console):

```
Scaffold-DbContext "server=localhost;database=shoeshop;user=root;password=1234" Pomelo.EntityFrameworkCore.MySql -OutputDir Entities -ContextDir DbContexts -Context ShoeshopContext -Force
```

**Что создаётся:**
- Папка `Entities/` — классы для каждой таблицы
- Папка `DbContexts/` — класс `ShoeshopContext` (главный класс для работы с БД)

**Запускай `-Force` каждый раз, когда меняешь БД** — перегенерирует всё заново.

---

## Шаг 3 — Сущности (Entity классы)

После scaffold появятся классы вроде:

```csharp
// Entities/Product.cs (СГЕНЕРИРОВАННЫЙ)
public partial class Product
{
    public int IdProd { get; set; }
    public string? Article { get; set; }
    public int? ProdName { get; set; }
    public decimal Price { get; set; }
    public int? Sale { get; set; }
    public int Count { get; set; }
    public string? Descrip { get; set; }
    public string? Image { get; set; }
    public int? SupId { get; set; }
    public int? ManufId { get; set; }
    public int? CatId { get; set; }

    // Навигационные свойства
    public virtual Category? Cat { get; set; }
    public virtual Manufacrurer? Manuf { get; set; }
    public virtual Supplier? Sup { get; set; }
}
```

Обрати внимание: класс `partial` — это позволяет расширить его в другом файле.  
В проекте добавлены вычисляемые свойства (`HasDiscount`, `FinalPrice`) в отдельном файле.

---

## Шаг 4 — DbContext

`ShoeshopContext` — это главный класс для доступа к БД. Он содержит `DbSet<T>` для каждой таблицы:

```csharp
public partial class ShoeshopContext : DbContext
{
    // Каждое свойство = таблица в БД
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Supplier> Suppliers { get; set; }
    // ... и т.д.

    // Строка подключения к БД
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(
            "server=localhost;database=shoeshop;user=root;password=1234",
            new MySqlServerVersion(new Version(8, 0, 41))
        );
    }
}
```

---

## Шаг 5 — CRUD операции

### Создать контекст:

```csharp
using var db = new ShoeshopContext();
// После using db автоматически закрывается
```

### READ — Чтение данных:

```csharp
// Все продукты
List<Product> allProducts = db.Products.ToList();

// С подгрузкой связанных объектов (Include = JOIN)
var products = db.Products
    .Include(p => p.Cat)        // подгрузить категорию
    .Include(p => p.Manuf)      // подгрузить производителя
    .Include(p => p.Sup)        // подгрузить поставщика
    .ToList();

// Один элемент по условию
var user = db.Users.FirstOrDefault(u => u.Email == "test@mail.ru");
```

### CREATE — Добавить новую запись:

```csharp
using var db = new ShoeshopContext();

Product newProduct = new Product
{
    Article = "ART-001",
    Descrip = "Кроссовки мужские",
    Price = 3500,
    Sale = 0,
    Count = 10,
    CatId = 1,
    ManufId = 2,
    SupId = 1
};

db.Products.Add(newProduct);  // подготовить к добавлению
db.SaveChanges();              // выполнить INSERT в БД
```

### UPDATE — Изменить запись:

```csharp
using var db = new ShoeshopContext();

// Найти нужный объект
Product product = db.Products.FirstOrDefault(p => p.IdProd == 5);

if (product != null)
{
    // Изменить поля
    product.Price = 4000;
    product.Sale = 10;
    product.Descrip = "Новое описание";

    db.SaveChanges();  // выполнить UPDATE в БД
}
```

### DELETE — Удалить запись:

```csharp
using var db = new ShoeshopContext();

Product product = db.Products.FirstOrDefault(p => p.IdProd == 5);

if (product != null)
{
    // Проверка: нельзя удалить если есть заказы
    bool hasOrders = db.Prodorders.Any(po => po.ProdId == product.IdProd);
    
    if (hasOrders)
    {
        MessageBox.Show("Невозможно удалить: товар есть в заказах");
        return;
    }

    db.Products.Remove(product);  // подготовить к удалению
    db.SaveChanges();              // выполнить DELETE
}
```

---

## Include — подгрузка связанных данных

Без `Include` навигационные свойства будут `null`:

```csharp
var product = db.Products.FirstOrDefault(p => p.IdProd == 1);
Console.WriteLine(product.Cat.CatName); // ОШИБКА! Cat == null
```

С `Include`:
```csharp
var product = db.Products
    .Include(p => p.Cat)   // теперь Cat загружен из БД
    .FirstOrDefault(p => p.IdProd == 1);

Console.WriteLine(product.Cat.CatName); // OK!
```

---

## Реальный пример из проекта — CatalogWindow

```csharp
private ShoeshopContext db = new ShoeshopContext();

private void LoadProducts()
{
    var products = db.Products
        .Include(p => p.Cat)
        .Include(p => p.Manuf)
        .Include(p => p.Sup)
        .Include(p => p.ProdNameNavigation)
        .ToList();

    ProductList.ItemsSource = products;
}
```

---

## Реальный пример — AddProdWindow (добавление товара)

```csharp
private void SaveBtn_Click(object sender, RoutedEventArgs e)
{
    // Валидация
    if (string.IsNullOrEmpty(DescripBox.Text))
    {
        MessageBox.Show("Введите описание");
        return;
    }

    if (!decimal.TryParse(PriceBox.Text, out decimal price))
    {
        MessageBox.Show("Некорректная цена");
        return;
    }

    using var db = new ShoeshopContext();

    Product product = new Product
    {
        Descrip = DescripBox.Text,
        Price = price,
        Sale = int.Parse(SaleBox.Text),
        Count = int.Parse(CountBox.Text),
        CatId = (CatCombo.SelectedItem as Category)?.IdCat,
        ManufId = (ManufCombo.SelectedItem as Manufacrurer)?.IdManuf,
        SupId = (SupCombo.SelectedItem as Supplier)?.IdSup,
        Image = _selectedImageName  // имя файла картинки
    };

    db.Products.Add(product);
    db.SaveChanges();

    MessageBox.Show("Товар добавлен!");
    this.Close();
}
```

---

## Загрузка данных для ComboBox

В окнах добавления/редактирования нужно заполнить выпадающие списки:

```csharp
public AddProdWindow()
{
    InitializeComponent();
    
    using var db = new ShoeshopContext();
    
    // Заполнить ComboBox категориями
    CatCombo.ItemsSource = db.Categories.ToList();
    CatCombo.DisplayMemberPath = "CatName";  // показывать поле CatName

    // Заполнить производителями
    ManufCombo.ItemsSource = db.Manufacrurers.ToList();
    ManufCombo.DisplayMemberPath = "ManufName";

    // Заполнить поставщиками
    SupCombo.ItemsSource = db.Suppliers.ToList();
    SupCombo.DisplayMemberPath = "SupName";
}
```

---

## Шпаргалка CRUD

```csharp
using var db = new ShoeshopContext();

// READ
var list = db.Products.ToList();
var list = db.Products.Include(p => p.Cat).ToList();
var one  = db.Products.FirstOrDefault(p => p.IdProd == id);

// CREATE
db.Products.Add(newProduct);
db.SaveChanges();

// UPDATE
product.Price = 999;
db.SaveChanges();

// DELETE
db.Products.Remove(product);
db.SaveChanges();
```
