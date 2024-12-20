using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kasd_labs_console
{
    public interface MyIterator<T>
    {
        bool HasNext();
        T Next();
        void Remove();
    }
}
namespace kasd_labs_console.extended
{
    public interface MyIterator<T> : kasd_labs_console.MyIterator<T>
    {
        // Проверяет, есть ли предыдущий элемент
        bool HasPrevious();

        // Возвращает предыдущий элемент
        T Previous();

        // Возвращает индекс следующего элемента
        int NextIndex();

        // Возвращает индекс предыдущего элемента
        int PreviousIndex();

        // Заменяет последний возвращенный элемент на новый
        void Set(T element);

        // Добавляет новый элемент в коллекцию перед текущим элементом
        void Add(T element);
    }

}