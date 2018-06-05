using System;
using System.Collections.Generic;
using FreshMvvm;
using Xamarin.Forms;

namespace TMDBMobile.Core.Utils
{
    public static class TabbedNavigationContainerExtension
    {
        public static void RemoveTab<T>(this FreshTabbedNavigationContainer container)
        {
            (container.TabbedPages as List<Page>).RemoveAll(p => p is T);

            NavigationPage pageToRemove = null;

            foreach (var child in container.Children)
            {
                if (!(child is NavigationPage navigationPage))
                    continue;

                if (navigationPage.RootPage is T)
                {
                    pageToRemove = navigationPage;
                    break;
                }
            }

            if (pageToRemove != null)
                container.Children.Remove(pageToRemove);
        }
    }
}
