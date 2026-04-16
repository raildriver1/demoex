# WPF и XAML — интерфейс приложения

---

## Что такое WPF и XAML

**WPF** (Windows Presentation Foundation) — фреймворк для создания оконных приложений в .NET.  
**XAML** — XML-подобный язык разметки для описания UI. Как HTML, только для Windows-приложений.

Каждое окно = два файла:
- `LoginWin.xaml` — разметка (что нарисовано)
- `LoginWin.xaml.cs` — логика (что происходит при нажатии кнопок)

---

## Структура XAML-файла

```xml
<Window x:Class="ShoeShop.Windows.LoginWin"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Авторизация" Height="400" Width="350"
        WindowStartupLocation="CenterScreen"
        ResizeMode="NoResize">

    <!-- Содержимое окна -->
    <Grid>
        ...
    </Grid>

</Window>
```

`x:Class` — указывает, какой C# класс управляет этим окном.  
`xmlns` — пространства имён (просто копируй, не думай об этом).  
`Title`, `Height`, `Width` — свойства окна.

---

## Основные контейнеры (Layout)

### Grid — сетка (самый часто используемый)

```xml
<Grid>
    <!-- Объявляем строки -->
    <Grid.RowDefinitions>
        <RowDefinition Height="50"/>   <!-- фиксированная высота -->
        <RowDefinition Height="*"/>    <!-- занимает оставшееся место -->
        <RowDefinition Height="Auto"/> <!-- по содержимому -->
    </Grid.RowDefinitions>

    <!-- Объявляем колонки -->
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="200"/>
        <ColumnDefinition Width="*"/>
    </Grid.ColumnDefinitions>

    <!-- Элементы размещаем по строкам/колонкам -->
    <TextBlock Grid.Row="0" Grid.Column="0" Text="Заголовок"/>
    <Button Grid.Row="1" Grid.Column="1" Content="Нажми"/>
</Grid>
```

### StackPanel — складывает элементы в стопку

```xml
<!-- Вертикально (по умолчанию) -->
<StackPanel>
    <TextBlock Text="Строка 1"/>
    <TextBlock Text="Строка 2"/>
    <Button Content="Кнопка"/>
</StackPanel>

<!-- Горизонтально -->
<StackPanel Orientation="Horizontal">
    <Button Content="Да"/>
    <Button Content="Нет"/>
</StackPanel>
```

---

## Основные элементы управления

### TextBlock — просто текст (только для отображения)

```xml
<TextBlock Text="Привет мир"/>
<TextBlock Text="Жирный" FontWeight="Bold" FontSize="16"/>
<TextBlock Text="Зелёный" Foreground="Green"/>
```

### TextBox — поле ввода

```xml
<TextBox x:Name="EmailBox" 
         Width="200" 
         Height="30"
         Margin="5"/>

<!-- Пароль (скрывает символы) -->
<PasswordBox x:Name="PasswordBox" Width="200" Height="30"/>
```

`x:Name` — имя элемента. По нему обращаться из C#: `EmailBox.Text`

### Button — кнопка

```xml
<Button Content="Войти" 
        Width="100" Height="35"
        Click="LoginBtn_Click"/>
```

`Click="LoginBtn_Click"` — при нажатии вызывается метод `LoginBtn_Click` в C# файле.

### ComboBox — выпадающий список

```xml
<ComboBox x:Name="SupplierFilter" 
          Width="150"
          SelectionChanged="SupplierFilter_SelectionChanged"
          DisplayMemberPath="SupName"/>
```

`DisplayMemberPath` — какое свойство объекта показывать в списке.

### ListBox — список элементов

```xml
<ListBox x:Name="ProductList" 
         SelectionChanged="ProductList_SelectionChanged">
    <ListBox.ItemTemplate>
        <!-- шаблон как выглядит каждый элемент -->
        <DataTemplate>
            <TextBlock Text="{Binding Descrip}"/>
        </DataTemplate>
    </ListBox.ItemTemplate>
</ListBox>
```

### Image — картинка

```xml
<Image x:Name="ProductImage" 
       Width="300" Height="200"
       Stretch="Uniform"/>
```

---

## Margin и Padding

```xml
<!-- Margin — внешний отступ (снаружи элемента) -->
<Button Margin="10"/>           <!-- 10 со всех сторон -->
<Button Margin="10,5"/>         <!-- 10 лево/право, 5 сверху/снизу -->
<Button Margin="5,10,5,10"/>    <!-- лево, сверх, право, снизу -->

<!-- Padding — внутренний отступ (внутри элемента) -->
<Button Padding="15,5"/>
```

---

## Visibility — показать/скрыть

```xml
<Button x:Name="AddBtn" Visibility="Collapsed"/>  <!-- скрыта -->
<Button x:Name="AddBtn" Visibility="Visible"/>    <!-- видна -->
```

Из C#:
```csharp
AddBtn.Visibility = Visibility.Visible;
AddBtn.Visibility = Visibility.Collapsed;
```

---

## C# код окна — базовая структура

```csharp
using System.Windows;

namespace ShoeShop.Windows
{
    public partial class LoginWin : Window
    {
        // Конструктор окна
        public LoginWin()
        {
            InitializeComponent(); // ОБЯЗАТЕЛЬНО — инициализирует XAML
        }

        // Обработчик нажатия кнопки
        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Введите email");
                return;
            }

            // ... логика
        }
    }
}
```

---

## Открыть новое окно

```csharp
// Открыть окно и закрыть текущее
CatalogWindow catalog = new CatalogWindow();
catalog.Show();
this.Close();

// Открыть как диалог (блокирует родительское окно)
AddProdWindow addWin = new AddProdWindow();
addWin.ShowDialog();
```

---

## Реальный пример — LoginWin из проекта

**XAML:**
```xml
<Window Title="Авторизация" Height="400" Width="350">
    <StackPanel Margin="20">
        <TextBlock Text="Вход в систему" FontSize="18" HorizontalAlignment="Center"/>
        
        <TextBlock Text="Email:" Margin="0,10,0,2"/>
        <TextBox x:Name="EmailBox" Height="30"/>
        
        <TextBlock Text="Пароль:" Margin="0,10,0,2"/>
        <PasswordBox x:Name="PasswordBox" Height="30"/>
        
        <Button Content="Войти" Height="35" Margin="0,15,0,0"
                Click="LoginBtn_Click"/>
        
        <Button Content="Войти как гость" Height="35" Margin="0,5,0,0"
                Click="GuestBtn_Click"/>
    </StackPanel>
</Window>
```

**C#:**
```csharp
private void LoginBtn_Click(object sender, RoutedEventArgs e)
{
    using var db = new ShoeshopContext();
    
    var user = db.Users.FirstOrDefault(u => 
        u.Email == EmailBox.Text && 
        u.Passw == PasswordBox.Password);

    if (user == null)
    {
        MessageBox.Show("Неверный email или пароль");
        return;
    }

    // Сохранить данные пользователя
    CurrentUser.Id = user.IdUser;
    CurrentUser.FullName = $"{user.LastName} {user.FirstName}";
    CurrentUser.Role = db.Usersroles.FirstOrDefault(r => r.IdRole == user.RoleId)?.RoleName;

    // Открыть главное окно
    new CatalogWindow().Show();
    this.Close();
}
```

---

## Шпаргалка элементов

| Элемент | Назначение | Как получить значение |
|---------|-----------|----------------------|
| `TextBlock` | Показать текст | нельзя (только display) |
| `TextBox` | Ввод текста | `TextBox.Text` |
| `PasswordBox` | Ввод пароля | `PasswordBox.Password` |
| `Button` | Кнопка | событие `Click` |
| `ComboBox` | Выпадающий список | `ComboBox.SelectedItem` |
| `ListBox` | Список | `ListBox.SelectedItem` |
| `Image` | Картинка | `Image.Source = ...` |
| `CheckBox` | Флажок | `CheckBox.IsChecked` |
