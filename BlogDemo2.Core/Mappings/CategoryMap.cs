using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogDemo2.Core.Objects;
using FluentNHibernate.Mapping;

namespace BlogDemo2.Core.Mappings
{
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Id(x => x.Id);

            Map(x => x.Name)
                .Length(50)
                .Not.Nullable();

            Map(x => x.UrlSlug)
                .Length(50)
                .Not.Nullable();

            Map(x => x.Description)
                .Length(200);

            HasMany(x => x.Posts)
                .Inverse()
                .Cascade.All()
                .KeyColumn("Category");
        }
    }
}
