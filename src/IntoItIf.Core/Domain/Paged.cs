namespace IntoItIf.Core.Domain
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using Options;

   public class Paged<T> : IPaged<T>
   {
      #region Constructors and Destructors

      public Paged(
         Option<IEnumerable<T>> source,
         Option<int> pageIndex,
         Option<int> pageSize,
         Option<PageIndexFrom> indexFrom,
         Option<int> totalItemsCount)
      {
         var realSource = source.ReduceOrDefault() ?? throw new ArgumentNullException(nameof(source));
         PageIndex = pageIndex.ReduceOrDefault();
         PageSize = pageSize.ReduceOrDefault();
         IndexFrom = indexFrom.ReduceOrDefault();
         if (IndexFrom.Id > PageIndex)
         {
            throw new ArgumentException(
               $"indexFrom ({IndexFrom.Id}) > pageIndex ({pageIndex}), must indexFrom <= pageIndex");
         }

         TotalItemsCount = totalItemsCount.ReduceOrDefault();
         PagesCount = (int)Math.Ceiling(TotalItemsCount / (double)PageSize);
         Items = realSource.Skip((PageIndex - IndexFrom.Id) * PageSize)
            .Take(PageSize)
            .ToList();
      }

      #endregion

      #region Public Properties

      public bool HasNextPage => PageIndex - IndexFrom.Id + 1 < PagesCount;
      public bool HasPreviousPage => PageIndex - IndexFrom.Id > 0;
      public PageIndexFrom IndexFrom { get; }
      public IList<T> Items { get; }
      public int PageIndex { get; }
      public int PagesCount { get; }
      public int PageSize { get; }
      public int TotalItemsCount { get; }

      #endregion
   }
}