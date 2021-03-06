namespace IntoItIf.Core.Domain
{
   using Options;

   public abstract class InjecterGetter
   {
      #region Static Fields

      private static Option<IMapperService> _mapperService;

      #endregion

      #region Methods

      internal static Option<IMapperService> GetBaseMapperService()
      {
         return _mapperService;
      }

      public static Option<bool> SetBaseMapperService(Option<IMapperService> mapperService)
      {
         var realClassMapperService = _mapperService.ReduceOrDefault();
         var realMapperService = mapperService.ReduceOrDefault();
         if (realClassMapperService == null || realClassMapperService.GetType() != realMapperService.GetType())
         {
            _mapperService = mapperService;
         }

         return true;
      }

      #endregion
   }
}