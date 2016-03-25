using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace FileStorage
{
    public class XStorage
    {
        #region fields

        static XStorage _instance;
        readonly DirectoryInfo _dataFolder;
        Dictionary<IStorageItem, FileInfo> _itemFiles;

        #endregion

        #region ctors
        static XStorage()
        {
            _instance = new XStorage();
        }

        private XStorage()
        {
            _dataFolder = new DirectoryInfo(".").CreateSubdirectory("CategoriesStorage");
            _itemFiles = new Dictionary<IStorageItem, FileInfo>();
        }

        #endregion

        #region properties

        public static XStorage Instance
        {
            get { return _instance; }
        }

        #endregion

        #region methods

        public IEnumerable<IStorageItem> LoadItems()
        {
            var items = new List<IStorageItem>();
            var files = _dataFolder.GetFiles("*.cat");
            foreach(var finfo in files)
            {
                items.Add(LoadItem(finfo));
            }

            return items;
        }

        private IStorageItem LoadItem(FileInfo finfo)
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

        public void SaveItem(IStorageItem item)
        {
            FileInfo finfo;

           if( !_itemFiles.TryGetValue(item, out finfo))
           {
                finfo = GenerateNewFileName();
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

        public void DeleteItem(IStorageItem item)
        {
            FileInfo finfo;
            if(_itemFiles.TryGetValue(item, out finfo))
            {
                finfo.Delete();
                _itemFiles.Remove(item);
            }
            else
            {
                throw new ArgumentException("Нельзя найти файл удаляемого аргумента Item");
            }
        }

        public void SaveChange()
        {
            Parallel.ForEach(_itemFiles, item => SaveItem(item.Key));
        }

        private FileInfo GenerateNewFileName()
        {
            Int32 i = _itemFiles.Count + 1;
            FileInfo finfo;
            do
            {
                finfo = new FileInfo(String.Format("{0}\\{1}.cat", _dataFolder.FullName, i));
                i++;
            }
            while (_itemFiles.ContainsValue(finfo));

            return finfo;
        } 
        #endregion
    }
}
