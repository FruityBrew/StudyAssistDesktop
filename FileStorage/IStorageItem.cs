using System;

namespace FileStorage
{
    /// <summary>
    /// Интерфейс сохраняемой записи.
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
