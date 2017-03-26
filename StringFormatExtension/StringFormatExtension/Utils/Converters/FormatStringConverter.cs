using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace HondarerSoft.Utils.Converters
{
    /// <summary>
    /// 文字列のフォーマットを行うコンバーターを提供します。
    /// </summary>
    public class FormatStringConverter : IMultiValueConverter
    {
        /// <summary>
        /// フォーマット文字列を表す引数のインデックスを表します。
        /// </summary>
        private const int VALUE_INDEX_FORMAT = 0;

        /// <summary>
        /// フォーマット文字列と値をフォーマットします。
        /// </summary>
        /// <param name="values">フォーマット文字列と、フォーマット対象のオブジェクト。</param>
        /// <param name="targetType">バインディング ターゲット プロパティの型。</param>
        /// <param name="parameter">使用するコンバーター パラメーター。</param>
        /// <param name="culture">コンバーターで使用するカルチャ。</param>
        /// <returns>変換された値。不正なパラメーターの場合は <c>null</c> を、フォーマットで例外が発生した場合は例外文字列を返します。</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((values == null) || (values.Length == 0))
            {
                return null;
            }

            string format = values[VALUE_INDEX_FORMAT].ToString();

            if (values.Length == (VALUE_INDEX_FORMAT + 1))
            {
                // フォーマットの必要がない場合は、そのまま入力を返却
                return format;
            }
            else
            {
                // string.Format にかける
                try
                {
                    return string.Format(format, values.Skip(1).ToArray());
                }
                catch (Exception ex)
                {
                    return string.Format("[FormatException]\r\n{0}", ex.ToString());
                }
            }
        }

        /// <summary>
        /// バインディング ターゲット値をソース値に変換します。
        /// このメソッドは未実装です。常に <see cref="NotSupportedException"/> を返します。
        /// </summary>
        /// <param name="value">バインディング ターゲットによって生成される値。</param>
        /// <param name="targetTypes">変換する型の配列。配列の長さは、メソッドの戻り値として推奨されている値の数と型を示します。</param>
        /// <param name="parameter">使用するコンバーター パラメーター。</param>
        /// <param name="culture">コンバーターで使用するカルチャ。</param>
        /// <returns>このメソッドは未実装です。常に <see cref="NotSupportedException"/> を返します。</returns>
        /// <exception cref="NotSupportedException">このメソッドは未実装です。常に <see cref="NotSupportedException"/> を返します。</exception>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
