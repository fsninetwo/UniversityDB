using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityGame
{
    interface DAO <T>
    {
        void DeleteById(T item);
        void Insert(T item);
        void UpdateById(T item);
        int SelectId(string name, string path);
        List<string> SelectToComboBox(string path);
        List<T> SelectItems();
        List<T> SelectItemsByText(string text);
    }
}
