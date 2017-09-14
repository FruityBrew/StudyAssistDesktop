using System;

namespace FileStorage
{
    /// <summary>
    /// Интерфейс сохраняемого Элемента.
    /// </summary>
    public interface IStorageItem
    {
        /// <summary>
        /// Сохраняет элемент.
        /// </summary>
        Action<IStorageItem> SaveItem { get; set; }

        /// <summary>
        /// Удаляет элемент.
        /// </summary>
        Action<IStorageItem> DeleteItem { get; set; }
    }
}
