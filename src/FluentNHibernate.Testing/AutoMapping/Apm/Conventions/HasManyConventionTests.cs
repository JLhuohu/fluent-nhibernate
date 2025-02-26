﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentNHibernate.Automapping;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using NUnit.Framework;

namespace FluentNHibernate.Testing.AutoMapping.Apm.Conventions;

[TestFixture]
public class HasManyConventionTests
{
    [Test]
    public void ShouldBeAbleToSpecifyKeyColumnNameInConvention()
    {
        var model =
            AutoMap.Source(new StubTypeSource(typeof(Target)))
                .Conventions.Add<HasManyConvention>();

        model.BuildMappings()
            .First()
            .Classes.First()
            .Collections.First()
            .Key.Columns.First().Name.ShouldEqual("xxx");
    }

    [Test]
    public void ShouldBeAbleToSpecifyKeyColumnNameUsingForeignKeyConvention()
    {
        var model =
            AutoMap.Source(new StubTypeSource(typeof(Target)))
                .Conventions.Add<FKConvention>();

        model.BuildMappings()
            .First()
            .Classes.First()
            .Collections.First()
            .Key.Columns.First().Name.ShouldEqual("Targetxxx");
    }

    private class FKConvention : ForeignKeyConvention
    {
        protected override string GetKeyName(Member property, Type type)
        {
            return type.Name + "xxx";
        }
    }

    private class HasManyConvention : IHasManyConvention
    {
        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Key.Column("xxx");
        }
    }
}

internal class Target
{
    public int Id { get; set; }
    public IList<Child> Children { get; set; }
    public Child Child { get; set; }
}

internal class Child
{
    public int Id { get; set; }
}
