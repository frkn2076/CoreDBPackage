using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading;
using System.Xml.Linq;

namespace CoreDBPackage.Config {
    public static class MyCache {
        private static MemoryCache memoryCache = null;
        private static readonly object padlock = new object();
        private static MemoryCache MemoryCache {
            get {
                if (memoryCache == null) {
                    lock (padlock) {
                        if (memoryCache == null) {
                            memoryCache = new MemoryCache(new MemoryCacheOptions());
                        }
                    }
                }
                return memoryCache;
            }
        }
        public static void setObject<T>(string key, T obj) {
            MemoryCache.Set<T>(key, obj, DateTime.Now.AddMinutes(15));
        }
        public static T getObject<T>(string key) {
            return MemoryCache.Get<T>(key);
        }
        public static string getSetting(string key, bool isFirstCall = true) {
            var valuePair = MemoryCache.Get<(string,string)>(key);
            var language = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToUpper();
            var value = language == Constants.TurkishLetters ? valuePair.Item1 : valuePair.Item2;
            if(value == null && isFirstCall) {
                getAllSettings();
                return getSetting(key, false);
            }
            else {
                return value;
            }
        }
        public static void getAllSettings() {
            foreach (XElement level1Element in XElement.Load("Config//settings.xml").Elements("Setting")) {
                var key = level1Element.Attribute("key").Value;
                var TRvalue = level1Element.Attribute("TRvalue").Value;
                var ENvalue = level1Element.Attribute("ENvalue").Value;

                MemoryCache.Set<(string,string)>(key, (TRvalue,ENvalue), DateTime.Now.AddDays(1));
            }
        }
    }
}
