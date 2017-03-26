using HondarerSoft.Utils.Converters;
using System;
using System.Windows.Data;
using System.Windows.Markup;

// based on @wonderful_panda
// http://qiita.com/wonderful_panda/items/a45ffaaca7f9c6e0d494

namespace HondarerSoft.Utils.MarkupExtensions
{
    /// <summary>
    /// フォーマット文字列拡張を提供します。
    /// </summary>
    public class StringFormatExtension : MarkupExtension
    {
        /// <summary>
        /// フォーマット文字列を保持します。
        /// </summary>
        private readonly object format;

        /// <summary>
        /// フォーマット対象オブジェクトを含んだオブジェクト配列を保持します。
        /// </summary>
        private readonly object[] args;

        /// <summary>
        /// 文字列のフォーマットを行うコンバーターを保持します。
        /// </summary>
        private static IMultiValueConverter converter = new FormatStringConverter();

        /// <summary>
        /// 1 つの項目をフォーマットする <see cref="StringFormatExtension"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="format">フォーマット文字列。</param>
        /// <param name="arg1">1 つ目のフォーマット対象オブジェクト。</param>
        public StringFormatExtension(object format, object arg1)
            : this(format, new object[] { arg1 })
        {
        }

        /// <summary>
        /// 2 つの項目をフォーマットする <see cref="StringFormatExtension"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="format">フォーマット文字列。</param>
        /// <param name="arg1">1 つ目のフォーマット対象オブジェクト。</param>
        /// <param name="arg2">2 つ目のフォーマット対象オブジェクト。</param>
        public StringFormatExtension(object format, object arg1, object arg2)
            : this(format, new object[] { arg1, arg2 })
        {
        }

        /// <summary>
        /// 3 つの項目をフォーマットする <see cref="StringFormatExtension"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="format">フォーマット文字列。</param>
        /// <param name="arg1">1 つ目のフォーマット対象オブジェクト。</param>
        /// <param name="arg2">2 つ目のフォーマット対象オブジェクト。</param>
        /// <param name="arg3">3 つ目のフォーマット対象オブジェクト。</param>
        public StringFormatExtension(object format, object arg1, object arg2, object arg3)
            : this(format, new object[] { arg1, arg2, arg3 })
        {
        }

        /// <summary>
        /// 複数の項目をフォーマットする <see cref="StringFormatExtension"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="format">フォーマット文字列。</param>
        /// <param name="args">フォーマット対象オブジェクトの配列。</param>
        /// <exception cref="ArgumentException"><see cref="format"/> が <see cref="string"/> でも <see cref="BindingBase"/> でもありません。</exception>
        public StringFormatExtension(object format, object[] args)
        {
            if ((format is string) == false && (format is BindingBase) == false)
            {
                throw new ArgumentException("format must be string or binding", "format");
            }

            this.format = format;
            this.args = args;
        }

        /// <summary>
        /// このマークアップ拡張機能で使用するターゲット プロパティの値として提供されるオブジェクトを返します。
        /// </summary>
        /// <param name="serviceProvider">マークアップ拡張機能のサービスを提供できるサービス プロバイダー ヘルパー。</param>
        /// <returns>拡張機能が適用されたプロパティに設定するオブジェクトの値。</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            MultiBinding mb = new MultiBinding()
            {
                Mode = BindingMode.OneWay
            };

            if (format is BindingBase)
            {
                // フォーマットがバインド可能な場合
                mb.Bindings.Add(format as BindingBase);
                mb.Converter = converter;
            }
            else
            {
#if false
                // リテラルの場合は、StringFormat にそのまま設定
                mb.StringFormat = format.ToString();
#else
                // StringFormat の機構を利用すると、
                // 例外が起きた時などの扱いがこのマークアップ拡張の動きと統一されないので
                // フォーマットリテラルでも、StringFormatConverter を使う
                BindingBase binding = null;
                binding = new Binding()
                {
                    Source = format
                };
                mb.Bindings.Add(binding);
                mb.Converter = converter;
#endif
            }

            foreach (object arg in args)
            {
                //BindingBase binding = (arg as BindingBase) ?? new Binding() { Source = arg };

                BindingBase binding = null;
                if (arg is BindingBase)
                {
                    binding = (arg as BindingBase);
                }
                else
                {
                    binding = new Binding()
                    {
                        Source = arg
                    };
                }

                mb.Bindings.Add(binding);
            }

            return mb.ProvideValue(serviceProvider);
        }
    }
}
