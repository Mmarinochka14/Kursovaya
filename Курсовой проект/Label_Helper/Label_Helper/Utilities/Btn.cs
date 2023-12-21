using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Label_Helper.Utilities
{
    // Класс Btn, наследующий RadioButton для создания пользовательской кнопки
    public class Btn : RadioButton
    {
        // Статический конструктор
        static Btn()
        {
            // Переопределение метаданных для использования стиля по умолчанию
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Btn), new FrameworkPropertyMetadata(typeof(Btn)));
        }
    }
}
