using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TestApp.MODEL;

namespace TestApp
{
    public partial class MainWindow : Window
    {
        private string _leftTop = ""; // Левый операнд
        private string _operation = ""; // Знак операции
        private string _rightTop = ""; // Правый операнд
        public List<History> Histories
        {
            get;
            set;
        }
        //List<string> calculateHistory = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            
            Histories = new List<History>();

            var h1 = new History {Date = DateTime.Now, Value = "1234+213=123"};
            var h2 = new History {Date = DateTime.Now, Value = "1234-12345=23"};
            var h3 = new History {Date = DateTime.Now, Value = "123*213=3"};
            
            Histories.Add(h1);
            Histories.Add(h2);
            Histories.Add(h3);

            DataContext = this;
            
            //Добавляем обработчик для всех кнопок на гриде
            foreach (UIElement c in LayoutRoot.Children)
            {
                if (c is Button)
                {
                    ((Button) c).Click += Button_Click;
                }
            }
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // Получаем текст кнопки
            string buttonText = (string) ((Button) e.OriginalSource).Content;
            
            // Добавляем его в текстовое поле
            MainTextBlock.Text += buttonText;
            //int num;
            
            // Пытаемся преобразовать его в число
            bool result = Int32.TryParse(buttonText, out var num);
            
            // Если текст - это число
            if (result)
            {
                // Если операция не задана
                if (_operation == "")
                {
                    // Добавляем к левому операнду
                    _leftTop += buttonText;
                }
                else
                {
                    // Иначе к правому операнду
                    _rightTop += buttonText;
                }
            }
            // Если было введено не число
            else
            {
                // Если равно, то выводим результат операции
                if (buttonText == "=")
                {
                    Update_RightTop();
                    MainTextBlock.Text += _rightTop;
                    _operation = "";
                }
                else if (buttonText == ",")
                {
                      
                }
                // Очищаем поле и переменные
                else if (buttonText == "Clear")
                {
                    _leftTop = "";
                    _rightTop = "";
                    _operation = "";
                    MainTextBlock.Text = "";
                    SideTextBlock.Text = "";
                }
                // Получаем операцию
                else
                {
                    // Если правый операнд уже имеется, то присваиваем его значение левому
                    // операнду, а правый операнд очищаем
                    if (_rightTop != "")
                    {
                        Update_RightTop();
                        _leftTop = _rightTop;
                        _rightTop = "";
                    }

                    _operation = buttonText;
                }
            }
        }

        // Обновляем значение правого операнда
        private void Update_RightTop()
        {
            int num1 = Int32.Parse(_leftTop);
            int num2 = Int32.Parse(_rightTop);
            // И выполняем операцию
            switch (_operation)
            {
                case "+":
                    _rightTop = (num1 + num2).ToString();
                    break;
                case "-":
                    _rightTop = (num1 - num2).ToString();
                    break;
                case "*":
                    _rightTop = (num1 * num2).ToString();
                    break;
                case "/":
                    _rightTop = (num1 / num2).ToString();
                    break;
            }
        }
    }
}