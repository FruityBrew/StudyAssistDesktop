using System;

namespace FileStorage
{
    public interface IStorageItem
    {
        Action<IStorageItem> SaveItem { get; set; }
        Action<IStorageItem> DeleteItem { get; set; }
    }
}
