using HondarerSoft.Utils;
using StringFormatExtension.Resources;

namespace StringFormatExtension.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class MainWindowViewModel : BindableBase
    {
        /// <summary>
        /// 
        /// </summary>
        private string title = null;

        /// <summary>
        /// 
        /// </summary>
        private string author = null;

        /// <summary>
        /// 
        /// </summary>
        private string format = null;

        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                SetProperty(ref title, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Author
        {
            get
            {
                return author;
            }
            set
            {
                SetProperty(ref author, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Format
        {
            get
            {
                return format;
            }
            set
            {
                SetProperty(ref format, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public MainWindowViewModel()
        {
            Title = "坊ちゃん";
            Author = "夏目漱石";
            Format = AssemblyResource.Author_of_0_is_1;
        }
    }
}
