using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp3.Helpers
{
    public static class MessageHelper
    {

        public static void ShowInfo(string info)
        {
            MessageBox.Show(info, "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void ShowError(string info)
        {
            MessageBox.Show(info, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void ShowWarning(string info)
        {
            MessageBox.Show(info, "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

    }
}
