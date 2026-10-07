using System;
using System.Collections.Generic;

namespace Assignment_08
{
    public delegate string BookFunction(Book B);

    public class LibraryEngine
    {
        public static void ProcessBooks(List<Book> bList, BookFunction fPtr)
        {
            foreach (Book B in bList)
            {
                Console.WriteLine(fPtr(B));
            }
        }

        public static void ProcessBooks(List<Book> bList, Func<Book, string> fPtr)
        {
            foreach (Book B in bList)
            {
                Console.WriteLine(fPtr(B));
            }
        }
    }
}
