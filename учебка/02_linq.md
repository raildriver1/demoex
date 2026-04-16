# LINQ — запросы к спискам и коллекциям

---

## Что такое LINQ

LINQ (Language Integrated Query) — это способ фильтровать, сортировать и преобразовывать коллекции прямо в C# коде.  
Думай о нём как о **SQL, но для списков**.

```csharp
using System.Linq;  // обязательно подключить
```

---

## Базовые методы — запомни эти 8

### 1. `Where` — фильтрация (как WHERE в SQL)

```csharp
List<Product> products = db.Products.ToList();

// Только продукты со скидкой
var withDiscount = products.Where(p => p.Sale > 0).ToList();

// Только товары в наличии
var inStock = products.Where(p => p.Count > 0).ToList();
```

**Синтаксис:** `.Where(элемент => условие)`  
`p` — это каждый элемент списка (можно назвать как угодно: `p`, `x`, `product`)  
`=>` — лямбда-стрелка ("для каждого p выполни...")

---

### 2. `OrderBy` / `OrderByDescending` — сортировка

```csharp
// По возрастанию количества
var sorted = products.OrderBy(p => p.Count).ToList();

// По убыванию количества
var sortedDesc = products.OrderByDescending(p => p.Count).ToList();
```

---

### 3. `Select` — преобразование (как SELECT в SQL)

```csharp
// Получить только названия продуктов
List<string> names = products.Select(p => p.Descrip).ToList();

// Получить только ID
List<int> ids = products.Select(p => p.IdProd).ToList();
```

---

### 4. `ToList()` — превратить в список

Большинство LINQ-запросов возвращают `IEnumerable<T>` — это "ленивый" запрос.  
`.ToList()` выполняет его и возвращает реальный `List<T>`.

```csharp
var result = db.Products.Where(p => p.Sale > 0).ToList(); // List<Product>
```

---

### 5. `FirstOrDefault` — первый элемент или null

```csharp
// Найти пользователя по email
User user = db.Users.FirstOrDefault(u => u.Email == inputEmail);

if (user == null)
{
    // пользователь не найден
}
```

---

### 6. `Any` — есть ли хоть один элемент?

```csharp
// Есть ли у продукта хоть один заказ?
bool hasOrders = db.Prodorders.Any(po => po.ProdId == product.IdProd);

if (hasOrders)
{
    MessageBox.Show("Нельзя удалить — есть заказы!");
}
```

---

### 7. `Count` — количество

```csharp
int total = products.Count();
int withSale = products.Count(p => p.Sale > 0);
```

---

### 8. `Contains` — содержит ли строка подстроку (для поиска)

```csharp
string searchText = "кросс";

var found = products.Where(p =>
    p.Descrip.ToLower().Contains(searchText.ToLower())
).ToList();
```

---

## Реальный пример из проекта — CatalogWindow

Вот как работает поиск + фильтр + сортировка в проекте:

```csharp
private void UpdateProductList()
{
    // Берём ВСЕ продукты из БД с подгрузкой связанных объектов
    var products = db.Products
        .Include(p => p.Cat)
        .Include(p => p.Manuf)
        .Include(p => p.Sup)
        .Include(p => p.ProdNameNavigation)
        .ToList();

    // Поиск по тексту
    string search = SearchBox.Text.ToLower();
    if (!string.IsNullOrEmpty(search))
    {
        products = products.Where(p =>
            (p.Descrip != null && p.Descrip.ToLower().Contains(search)) ||
            (p.Cat != null && p.Cat.CatName.ToLower().Contains(search)) ||
            (p.Manuf != null && p.Manuf.ManufName.ToLower().Contains(search))
        ).ToList();
    }

    // Фильтр по поставщику
    if (SupplierFilter.SelectedItem is Supplier selectedSup)
    {
        products = products.Where(p => p.SupId == selectedSup.IdSup).ToList();
    }

    // Сортировка
    if (SortAscBtn.IsChecked == true)
        products = products.OrderBy(p => p.Count).ToList();
    else if (SortDescBtn.IsChecked == true)
        products = products.OrderByDescending(p => p.Count).ToList();

    // Привязать результат к списку на экране
    ProductList.ItemsSource = products;
}
```

---

## Цепочки LINQ (можно объединять)

```csharp
var result = db.Products
    .Where(p => p.Sale > 0)          // только со скидкой
    .OrderByDescending(p => p.Sale)  // сначала самые дешёвые
    .Select(p => p.Descrip)          // только названия
    .ToList();                        // выполнить
```

---

## Шпаргалка

| Что нужно | Метод |
|-----------|-------|
| Отфильтровать | `.Where(x => условие)` |
| Сортировать А→Я | `.OrderBy(x => поле)` |
| Сортировать Я→А | `.OrderByDescending(x => поле)` |
| Первый или null | `.FirstOrDefault(x => условие)` |
| Есть ли хоть один | `.Any(x => условие)` |
| Преобразовать | `.Select(x => x.Поле)` |
| Превратить в список | `.ToList()` |
| Посчитать | `.Count()` |
