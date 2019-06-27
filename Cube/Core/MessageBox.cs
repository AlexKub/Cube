using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;

namespace Merlion.ECR.Update.Core.Win32_API
{
    /// <summary>
    /// Окно сообщения Win32
    /// </summary>
	public static class MessageBox
    {
        private const int IDOK = 1;

        private const int IDCANCEL = 2;

        private const int IDABORT = 3;

        private const int IDRETRY = 4;

        private const int IDIGNORE = 5;

        private const int IDYES = 6;

        private const int IDNO = 7;

        private const int DEFAULT_BUTTON1 = 0;

        private const int DEFAULT_BUTTON2 = 256;

        private const int DEFAULT_BUTTON3 = 512;

        private static MessageBoxResult Win32ToMessageBoxResult(int value)
        {
            switch (value)
            {
                case 1:
                    return MessageBoxResult.OK;
                case 2:
                    return MessageBoxResult.Cancel;
                case 6:
                    return MessageBoxResult.Yes;
                case 7:
                    return MessageBoxResult.No;
            }
            return MessageBoxResult.No;
        }

        /// <summary>Отображает окно сообщения с сообщением, заголовком, кнопкой и значком, которое принимает результат окна сообщения по умолчанию, совместимо с указанными параметрами и возвращает результат.</summary>
        /// <returns>Значение <see cref="T:System.Windows.MessageBoxResult" />, идентифицирующее кнопку, нажатую пользователем в окне сообщения.</returns>
        /// <param name="messageBoxText">Строка <see cref="T:System.String" />, задающая отображаемый текст.</param>
        /// <param name="caption">Строка <see cref="T:System.String" />, задающая отображаемый заголовок окна.</param>
        /// <param name="button">Значение <see cref="T:System.Windows.MessageBoxButton" />, определяющее, какие кнопки (кнопка) подлежат отображению.</param>
        /// <param name="icon">Значение <see cref="T:System.Windows.MessageBoxImage" />, задающее отображаемый значок.</param>
        /// <param name="defaultResult">Значение <see cref="T:System.Windows.MessageBoxResult" />, задающее результат окна сообщений по умолчанию.</param>
        /// <param name="options">Объект значений <see cref="T:System.Windows.MessageBoxOptions" />, задающий параметры.</param>
        [SecurityCritical]
        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options)
        {
            return MessageBox.ShowCore(IntPtr.Zero, messageBoxText, caption, button, icon, defaultResult, options);
        }

        /// <summary>Отображает окно сообщения с сообщением, заголовком, кнопкой и значком, которое принимает результат окна сообщения по умолчанию и возвращает результат.</summary>
        /// <returns>Значение <see cref="T:System.Windows.MessageBoxResult" />, идентифицирующее кнопку, нажатую пользователем в окне сообщения.</returns>
        /// <param name="messageBoxText">Строка <see cref="T:System.String" />, задающая отображаемый текст.</param>
        /// <param name="caption">Строка <see cref="T:System.String" />, задающая отображаемый заголовок окна.</param>
        /// <param name="button">Значение <see cref="T:System.Windows.MessageBoxButton" />, определяющее, какие кнопки (кнопка) подлежат отображению.</param>
        /// <param name="icon">Значение <see cref="T:System.Windows.MessageBoxImage" />, задающее отображаемый значок.</param>
        /// <param name="defaultResult">Значение <see cref="T:System.Windows.MessageBoxResult" />, задающее результат окна сообщений по умолчанию.</param>
        [SecurityCritical]
        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            return MessageBox.ShowCore(IntPtr.Zero, messageBoxText, caption, button, icon, defaultResult, MessageBoxOptions.None);
        }

        /// <summary>Отображает окно сообщения с сообщением, заголовком, кнопкой и значком, которое возвращает результат.</summary>
        /// <returns>Значение <see cref="T:System.Windows.MessageBoxResult" />, идентифицирующее кнопку, нажатую пользователем в окне сообщения.</returns>
        /// <param name="messageBoxText">Строка <see cref="T:System.String" />, задающая отображаемый текст.</param>
        /// <param name="caption">Строка <see cref="T:System.String" />, задающая отображаемый заголовок окна.</param>
        /// <param name="button">Значение <see cref="T:System.Windows.MessageBoxButton" />, определяющее, какие кнопки (кнопка) подлежат отображению.</param>
        /// <param name="icon">Значение <see cref="T:System.Windows.MessageBoxImage" />, задающее отображаемый значок.</param>
        [SecurityCritical]
        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            return MessageBox.ShowCore(IntPtr.Zero, messageBoxText, caption, button, icon, MessageBoxResult.None, MessageBoxOptions.None);
        }

        /// <summary>Отображает окно сообщения с сообщением, заголовком и кнопкой, которое возвращает результат.</summary>
        /// <returns>Значение <see cref="T:System.Windows.MessageBoxResult" />, идентифицирующее кнопку, нажатую пользователем в окне сообщения.</returns>
        /// <param name="messageBoxText">Строка <see cref="T:System.String" />, задающая отображаемый текст.</param>
        /// <param name="caption">Строка <see cref="T:System.String" />, задающая отображаемый заголовок окна.</param>
        /// <param name="button">Значение <see cref="T:System.Windows.MessageBoxButton" />, определяющее, какие кнопки (кнопка) подлежат отображению.</param>
        [SecurityCritical]
        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button)
        {
            return MessageBox.ShowCore(IntPtr.Zero, messageBoxText, caption, button, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);
        }

        /// <summary>Отображает окно сообщения с сообщением и заголовком, которое возвращает результат.</summary>
        /// <returns>Значение <see cref="T:System.Windows.MessageBoxResult" />, идентифицирующее кнопку, нажатую пользователем в окне сообщения.</returns>
        /// <param name="messageBoxText">Строка <see cref="T:System.String" />, задающая отображаемый текст.</param>
        /// <param name="caption">Строка <see cref="T:System.String" />, задающая отображаемый заголовок окна.</param>
        [SecurityCritical]
        public static MessageBoxResult Show(string messageBoxText, string caption)
        {
            return MessageBox.ShowCore(IntPtr.Zero, messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);
        }

        /// <summary>Отображает окно сообщения с сообщением, которое возвращает результат.</summary>
        /// <returns>Значение <see cref="T:System.Windows.MessageBoxResult" />, идентифицирующее кнопку, нажатую пользователем в окне сообщения.</returns>
        /// <param name="messageBoxText">Строка <see cref="T:System.String" />, задающая отображаемый текст.</param>
        [SecurityCritical]
        public static MessageBoxResult Show(string messageBoxText)
        {
            return MessageBox.ShowCore(IntPtr.Zero, messageBoxText, string.Empty, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);
        }

        private static int DefaultResultToButtonNumber(MessageBoxResult result, MessageBoxButton button)
        {
            if (result == MessageBoxResult.None)
            {
                return 0;
            }
            switch (button)
            {
                case MessageBoxButton.OK:
                    return 0;
                case MessageBoxButton.OKCancel:
                    if (result == MessageBoxResult.Cancel)
                    {
                        return 256;
                    }
                    return 0;
                case MessageBoxButton.YesNoCancel:
                    if (result == MessageBoxResult.No)
                    {
                        return 256;
                    }
                    if (result == MessageBoxResult.Cancel)
                    {
                        return 512;
                    }
                    return 0;
                case MessageBoxButton.YesNo:
                    if (result == MessageBoxResult.No)
                    {
                        return 256;
                    }
                    return 0;
            }
            return 0;
        }

        internal static MessageBoxResult ShowCore(IntPtr owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options)
        {
            if (!MessageBox.IsValidMessageBoxButton(button))
            {
                throw new InvalidEnumArgumentException("button", (int)button, typeof(MessageBoxButton));
            }
            if (!MessageBox.IsValidMessageBoxImage(icon))
            {
                throw new InvalidEnumArgumentException("icon", (int)icon, typeof(MessageBoxImage));
            }
            if (!MessageBox.IsValidMessageBoxResult(defaultResult))
            {
                throw new InvalidEnumArgumentException("defaultResult", (int)defaultResult, typeof(MessageBoxResult));
            }
            if (!MessageBox.IsValidMessageBoxOptions(options))
            {
                throw new InvalidEnumArgumentException("options", (int)options, typeof(MessageBoxOptions));
            }

            else if (owner == IntPtr.Zero)
            {
                owner = DllImport.user32.GetActiveWindow();
            }
            int type = (int)(button | (MessageBoxButton)icon | (MessageBoxButton)MessageBox.DefaultResultToButtonNumber(defaultResult, button) | (MessageBoxButton)options);
            return MessageBox.Win32ToMessageBoxResult(DllImport.user32.MessageBox(new HandleRef(null, owner), messageBoxText, caption, type));
        }

        private static bool IsValidMessageBoxButton(MessageBoxButton value)
        {
            return value == MessageBoxButton.OK || value == MessageBoxButton.OKCancel || value == MessageBoxButton.YesNo || value == MessageBoxButton.YesNoCancel;
        }

        private static bool IsValidMessageBoxImage(MessageBoxImage value)
        {
            return value == MessageBoxImage.Asterisk || value == MessageBoxImage.Hand || value == MessageBoxImage.Exclamation || value == MessageBoxImage.Hand || value == MessageBoxImage.Asterisk || value == MessageBoxImage.None || value == MessageBoxImage.Question || value == MessageBoxImage.Hand || value == MessageBoxImage.Exclamation;
        }

        private static bool IsValidMessageBoxResult(MessageBoxResult value)
        {
            return value == MessageBoxResult.Cancel || value == MessageBoxResult.No || value == MessageBoxResult.None || value == MessageBoxResult.OK || value == MessageBoxResult.Yes;
        }

        private static bool IsValidMessageBoxOptions(MessageBoxOptions value)
        {
            int num = -3801089;
            return (value & (MessageBoxOptions)num) == MessageBoxOptions.None;
        }
    }

    /// <summary>Определяет параметры отображения окна сообщения.</summary>
	[Flags]
    public enum MessageBoxOptions
    {
        /// <summary>Параметры не заданы.</summary>
        None = 0,
        /// <summary>Окно сообщения отображается на активном в настоящий момент рабочем столе, даже если пользователь не осуществил вход в систему.Указывает, что окно сообщения должно быть отображено из Microsoft .NET Framework служебного приложения для уведомления пользователя о событии.</summary>
        ServiceNotification = 2097152,
        /// <summary>Окно сообщения отображается на рабочем столе по умолчанию.Указывает, что окно сообщения должно быть отображено из Microsoft .NET Framework служебного приложения для уведомления пользователя о событии.</summary>
        DefaultDesktopOnly = 131072,
        /// <summary>Текст окна сообщения и заголовок окна выравниваются по правому краю.</summary>
        RightAlign = 524288,
        /// <summary>Весь текст, кнопки, пиктограммы и заголовки отображаются справа налево.</summary>
        RtlReading = 1048576
    }

    /// <summary>Задает значок, который отображается в окне сообщения.</summary>
	public enum MessageBoxImage
    {
        /// <summary>Значок не отображается.</summary>
        None,
        /// <summary>В окне сообщения отображается значок руки.</summary>
        Hand = 16,
        /// <summary>В окне сообщения отображается значок вопроса.</summary>
        Question = 32,
        /// <summary>В окне сообщения отображается значок восклицательного знака.</summary>
        Exclamation = 48,
        /// <summary>В окне сообщения отображается значок звездочки (*).</summary>
        Asterisk = 64,
        /// <summary>В окне сообщения отображается значок остановки.</summary>
        Stop = 16,
        /// <summary>В окне сообщения отображается значок ошибки.</summary>
        Error = 16,
        /// <summary>В окне сообщения отображается значок предупреждения.</summary>
        Warning = 48,
        /// <summary>В окне сообщения отображается значок данных.</summary>
        Information = 64
    }

    /// <summary>Указывает кнопку в окне сообщения, нажатую пользователем.</summary>
	public enum MessageBoxResult
    {
        /// <summary>Окно сообщения не возвращает никаких результатов.</summary>
        None,
        /// <summary>Полученное значение окна сообщения — ОК.</summary>
        OK,
        /// <summary>Полученное значение окна сообщения — Отмена.</summary>
        Cancel,
        /// <summary>Полученное значение окна сообщения — Да.</summary>
        Yes = 6,
        /// <summary>Полученное значение окна сообщения — Нет.</summary>
        No
    }

    /// <summary>Задает кнопки, отображаемые в окне сообщения.Используется в качестве аргумента метода <see cref="Overload:System.Windows.MessageBox.Show" />.</summary>
	public enum MessageBoxButton
    {
        /// <summary>В окне сообщения отображается кнопка ОК.</summary>
        OK,
        /// <summary>В окне сообщения отображаются кнопки ОК и Отмена.</summary>
        OKCancel,
        /// <summary>В окне сообщения отображаются кнопки Да, Нет, and Отмена.</summary>
        YesNoCancel = 3,
        /// <summary>В окне сообщения отображаются кнопки Да и Нет.</summary>
        YesNo
    }
}
