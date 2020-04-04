using Xunit;

namespace Opendata.Core.Tests
{
    public class DatasetMetadataTests
    {
        [Fact]
        public void ParseContent()
        {
            var metadata = new DatasetMetadata();
            var line =
                "property,value\r\nstandardversion,http://opendata.gosmonitor.ru/standard/3.0\r\nidentifier,3444200448-ForestDeath\r\ntitle,\"Сведения о гибели лесных насаждений\"\r\ndescription,\"Причины повреждения и гибели лесов и площадь поврежденных и погибших лесных насаждений, расположенных на территории Волгоградской области\"\r\ncreator,\"Комитет природных ресурсов, лесного хозяйства и экологии Волгоградской области\"\r\ncreated,20171123\r\nmodified,20190719\r\nsubject,\"Лесные насаждения, причины повреждения, гибель лесных насаждений, леса\"\r\nformat,CSV\r\nprovenance,Обновление набора открытых данных\r\nvalid,20200430\r\npublishername,Жукова Галина Геннадьевна\r\npublisherphone,+78442308953\r\npublishermbox,G_Zhukova@volganet.ru\r\ndata-2019-07-19-structure-2017-11-23,http://opendata.volganet.ru/3444200448-ForestDeath/data-2019-07-19-structure-2017-11-23.csv\r\ndata-2019-04-25-structure-2017-11-23,http://opendata.volganet.ru/3444200448-ForestDeath/data-2019-04-25-structure-2017-11-23.csv\r\ndata-2018-04-27-structure-2017-11-23,http://opendata.volganet.ru/3444200448-ForestDeath/data-2018-04-27-structure-2017-11-23.csv\r\ndata-2018-01-29-structure-2017-11-23,http://opendata.volganet.ru/3444200448-ForestDeath/data-2018-01-29-structure-2017-11-23.csv\r\ndata-2017-11-23-structure-2017-11-23,http://opendata.volganet.ru/3444200448-ForestDeath/data-2017-11-23-structure-2017-11-23.csv\r\nstructure-2017-11-23,http://opendata.volganet.ru/3444200448-ForestDeath/structure-2017-11-23.csv";
            metadata.ParseContent(line);

            Assert.True(string.IsNullOrEmpty(metadata.ErrorComment));
            Assert.Equal(20, metadata.Properties.Count);
            Assert.NotNull(metadata.Properties["standardversion"]);
            Assert.NotNull(metadata.Properties["identifier"]);
            Assert.NotNull(metadata.Properties["title"]);
            Assert.NotNull(metadata.Properties["description"]);
            Assert.NotNull(metadata.Properties["creator"]);
            Assert.NotNull(metadata.Properties["created"]);
            Assert.NotNull(metadata.Properties["modified"]);
            Assert.NotNull(metadata.Properties["subject"]);
            Assert.NotNull(metadata.Properties["format"]);
            Assert.NotNull(metadata.Properties["provenance"]);
            Assert.NotNull(metadata.Properties["valid"]);
            Assert.NotNull(metadata.Properties["publishername"]);
            Assert.NotNull(metadata.Properties["publisherphone"]);
            Assert.NotNull(metadata.Properties["publishermbox"]);
            Assert.NotNull(metadata.Properties["data-2019-07-19-structure-2017-11-23"]);
            Assert.NotNull(metadata.Properties["data-2019-04-25-structure-2017-11-23"]);
            Assert.NotNull(metadata.Properties["data-2018-04-27-structure-2017-11-23"]);
            Assert.NotNull(metadata.Properties["data-2018-01-29-structure-2017-11-23"]);
            Assert.NotNull(metadata.Properties["data-2017-11-23-structure-2017-11-23"]);
            Assert.NotNull(metadata.Properties["structure-2017-11-23"]);

            Assert.Equal("http://opendata.volganet.ru/3444200448-ForestDeath/data-2019-07-19-structure-2017-11-23.csv",
                metadata.DataUrl);
            Assert.Equal("http://opendata.volganet.ru/3444200448-ForestDeath/structure-2017-11-23.csv",
                metadata.StructureUrl);
        }
    }
}