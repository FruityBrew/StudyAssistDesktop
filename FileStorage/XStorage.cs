using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace FileStorage
{
    /// <summary>
    /// Хранилище записей (синглтон).
    /// </summary>
    public class XStorage
    {
        #region Fields

        /// <summary>
        /// Созданная сущность.
        /// </summary>
        private static readonly XStorage _instance;

        /// <summary>
        /// Путь папки для хранения файлов категорий.
        /// </summary>
        private readonly DirectoryInfo _dataFolder;

        /// <summary>
        /// Словарь элементов и файлов.
        /// </summary>
        private readonly Dictionary<IStorageItem, FileInfo> _itemFiles;

        #endregion Fields

        #region Сtors

        /// <summary>
        /// Статический конструктор.
        /// </summary>
        static XStorage()
        {
            _instance = new XStorage();
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        private XStorage()
        {
            _dataFolder = new DirectoryInfo(".")
                .CreateSubdirectory("CategoriesStorage");

            _itemFiles = new Dictionary<IStorageItem, FileInfo>();
        }

        #endregion Ctors

        #region Properties

        /// <summary>
        /// Возвращает сущность.
        /// </summary>
        public static XStorage Instance
        {
            get { return _instance; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Загружает хранимые записи.
        /// </summary>
        /// <returns>Последовательность хранимых записей.</returns>
        public IEnumerable<IStorageItem> LoadItems()
        {
            var files = _dataFolder.GetFiles("*.cat");

            return files.Select(_LoadItem).ToList();
        }

        /// <summary>
        /// Сохраняет запись в хранилище.
        /// </summary>
        /// <param name="item">Запись.</param>
        public void SaveItem(IStorageItem item)
        {
            if (!_itemFiles.TryGetValue(item, out FileInfo finfo))
            {
                finfo = _GenerateNewFileName();
            }

            try
            {
                var bformatter = new BinaryFormatter();
                using (var fstream = finfo.OpenWrite())
                {
                    bformatter.Serialize(fstream, item);
                }
            }
            //TODO
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Удаляет запись из хранилища.
        /// </summary>
        /// <param name="item">Удаляемая запись.</param>
        public void DeleteItem(IStorageItem item)
        {
            if (_itemFiles.TryGetValue(item, out FileInfo finfo))
            {
                finfo.Delete();
                _itemFiles.Remove(item);
            }
            else
            {
                throw new ArgumentException(
                    "Нельзя найти файл удаляемого аргумента Item");
            }
        }

        /// <summary>
        /// Сохраняет изменения.
        /// </summary>
        public void SaveChange()
        {
            Parallel.ForEach(_itemFiles, item => SaveItem(item.Key));
        }

        #endregion Methods

        #region Utilities

        /// <summary>
        /// Генерирует имя файла.
        /// </summary>
        /// <returns></returns>
        private FileInfo _GenerateNewFileName()
        {
            Int32 i = _itemFiles.Count + 1;
            FileInfo finfo;

            do
            {
                finfo = new FileInfo(String.Format(
                    "{0}\\{1}.cat", _dataFolder.FullName, i));
                i++;
            }
            while (_itemFiles.ContainsValue(finfo));

            return finfo;
        }

        /// <summary>
        /// Загружает хранимую запись.
        /// </summary>
        /// <param name="finfo">Файл с записью.</param>
        /// <returns>Хранимая запись.</returns>
        private IStorageItem _LoadItem(FileInfo finfo)
        {
            IStorageItem item;
            var bformatter = new BinaryFormatter();

            using (var fstream = finfo.OpenRead())
            {
                var obj = bformatter.Deserialize(fstream);
               
                item = (IStorageItem)obj;
                item.SaveItem = SaveItem;
                item.DeleteItem = DeleteItem;
            }

            _itemFiles[item] = finfo;
            return item;
        }

        #endregion Utilities
    }
}
