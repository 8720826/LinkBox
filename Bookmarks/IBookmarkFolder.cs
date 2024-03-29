using System.Collections.Generic;

namespace LinkBox.Bookmarks
{
    public interface IBookmarkFolder : IBookmarkItem, IEnumerable<IBookmarkItem>
    {

        IEnumerable<IBookmarkItem> AllItems { get; }
        IEnumerable<IBookmarkFolder> AllFolders { get; }
        void Add(IBookmarkItem item);

        IEnumerable<T> GetAllItems<T>() where T : class, IBookmarkItem;

    }
}