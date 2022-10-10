using System;
using Syncfusion.Maui.DataSource;
using Syncfusion.Maui.ListView;

namespace FireSignage
{
	public class Behavior : Behavior<SfListView>
    {
        #region Fields

        private SfListView ListView;

        #endregion

        #region Overrides
        protected override void OnAttachedTo(SfListView bindable)
        {
            ListView = bindable;
            ListView.DataSource.GroupDescriptors.Add(new GroupDescriptor()
            {
                PropertyName = "Category",
                KeySelector = (object obj1) =>
                {
                    var item = (obj1 as PreMadeSigns);
                    return item.Category[0].ToString();
                },
            });
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(SfListView bindable)
        {
            ListView = null;
            base.OnDetachingFrom(bindable);
        }
        #endregion
    }
}