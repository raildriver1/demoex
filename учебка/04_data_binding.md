# Data Binding — привязка данных в WPF

---

## Проблема без биндинга

Представь: у тебя 100 товаров в БД. Как их показать на экране?

**Без биндинга** пришлось бы так:
```csharp
// Для КАЖДОГО товара создавать элемент руками — это ад
TextBlock tb1 = new TextBlock();
tb1.Text = product1.Descrip;
panel.Children.Add(tb1);

TextBlock tb2 = new TextBlock();
tb2.Text = product2.Descrip;
panel.Children.Add(tb2);

// ... и так 100 раз
```

**С биндингом** — две строки:
```csharp
// C# — даём список
ProductList.ItemsSource = db.Products.ToList();
```
```xml
<!-- XAML — говорим что показывать -->
<TextBlock Text="{Binding Descrip}"/>
```

WPF сам перебирает список и рисует каждый товар. Ты ничего не делаешь руками.

---

## Как это работает — по шагам

### Шаг 1: В C# даёшь список ListBox-у

```csharp
// Берём все товары из БД
List<Product> tovary = db.Products.ToList();

// Говорим ListBox-у: "вот твои данные"
prodList.ItemsSource = tovary;
```

`ItemsSource` — это свойство ListBox-а. Буквально переводится "источник элементов".

---

### Шаг 2: В XAML описываешь как выглядит ОДИН товар

```xml
<ListBox x:Name="prodList">

    <ListBox.ItemTemplate>   <!-- шаблон для одного элемента -->
        <DataTemplate>       <!-- "Data" = данные, "Template" = шаблон -->

            <!-- Всё что здесь — это шаблон ОДНОГО товара -->
            <StackPanel>
                <TextBlock Text="{Binding Descrip}"/>
                <TextBlock Text="{Binding Price}"/>
            </StackPanel>

        </DataTemplate>
    </ListBox.ItemTemplate>

</ListBox>
```

WPF берёт этот шаблон и **повторяет его для каждого товара из списка**.

---

### Шаг 3: Что такое {Binding Descrip}

```xml
<TextBlock Text="{Binding Descrip}"/>
```

Читается так: **"возьми поле Descrip из текущего объекта и поставь сюда"**

"Текущий объект" — это тот Product, для которого сейчас рисуется шаблон.

Если в списке 10 товаров — шаблон нарисуется 10 раз, каждый раз с другим Product.

---

## Картинка в голове

```
ItemsSource = [Product1, Product2, Product3]
                  |           |          |
              шаблон      шаблон     шаблон
              -------      ------     ------
              Descrip1    Descrip2   Descrip3
              Price1      Price2     Price3
```

---

## Почему {Binding Descrip}, а не просто Descrip?

Потому что WPF должен понять что это не просто текст, а команда "возьми данные".

```xml
<TextBlock Text="Descrip"/>          <!-- покажет буквально слово "Descrip" -->
<TextBlock Text="{Binding Descrip}"/><!-- покажет значение поля Descrip из объекта -->
```

Фигурные скобки `{}` — это сигнал для WPF: "это не текст, это выражение".

---

## Откуда WPF знает что Descrip — это из Product?

Потому что `ItemsSource` содержит `List<Product>`.
WPF видит что список состоит из объектов `Product` и знает все их поля.

Поэтому `{Binding Descrip}` = `product.Descrip`, а `{Binding Price}` = `product.Price`.

---

## Связь между C# и XAML — через x:Name

```csharp
// C# — обращаемся по имени
prodList.ItemsSource = tovary;
//  ↑
//  имя
```

```xml
<!-- XAML — то же имя -->
<ListBox x:Name="prodList">
<!--              ↑
                  то же имя -->
```

`x:Name="prodList"` — это как переменная. Написал в XAML, используешь в C#. Всё.

---

## Итого — весь биндинг это 2 вещи

**1. В C# — один раз:**
```csharp
prodList.ItemsSource = db.Products.ToList();
```

**2. В XAML — внутри DataTemplate:**
```xml
<TextBlock Text="{Binding Descrip}"/>
<TextBlock Text="{Binding Price}"/>
<TextBlock Text="{Binding Count}"/>
```

Названия после `Binding` = названия свойств в классе `Product`.  
Хочешь показать другое поле — пишешь другое название. Всё.
