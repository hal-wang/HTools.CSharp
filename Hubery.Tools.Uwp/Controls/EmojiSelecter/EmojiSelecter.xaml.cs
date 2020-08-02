﻿using Microsoft.Toolkit.Uwp;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace Hubery.Tools.Uwp.Controls.EmojiSelecter
{
    public sealed partial class EmojiSelecter : UserControl
    {
        internal IncrementalLoadingCollection<EmojiSource, string> Emojis { get; } = new IncrementalLoadingCollection<EmojiSource, string>();

        public EmojiSelecter()
        {
            this.InitializeComponent();
        }

        public event TypedEventHandler<EmojiSelecter, EmojiChangedEventArgs> Changed;

        private string _old = null;
        private void Emoji_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as string;
            Changed?.Invoke(this, new EmojiChangedEventArgs()
            {
                New = item,
                Old = _old
            });
            _old = item;
        }
    }
}