using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xunit;
using System.Threading.Tasks;
using henri_vaucher_API.Models;
using System.Text.Json;
using System.IO;
using System;

namespace XUnitTest_henri_vaucher
{
    public class PicturesController
    {

        // Tester le statut du code OK /api/pictures
        [Fact]
        public async Task Get_WhenCalled_ReturnsOkResultAsync()
        {
            // Préparation de l'appel à l'API
            HttpClient client = new HttpClient();

            var content = await client.GetAsync("http://localhost:5010/" + "api/Pictures/");

            // Act
            var result = content.StatusCode;
            // Assert
            Assert.IsType<HttpStatusCode>(result);
            Assert.Equal(HttpStatusCode.OK, result);
        }

        // Tester si on reçoit des objets /api/pictures
        [Fact]
        public async Task Get_WhenCalled_ReturnsAllItemsAsync()
        {
            // Préparation de l'appel à l'API
            HttpClient client = new HttpClient();

            // Act
            Stream streamTask = await client.GetStreamAsync("http://localhost:5010/" + "api/Pictures/");
            List<Picture> pictures = await JsonSerializer.DeserializeAsync<List<Picture>>(streamTask,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );

            // Assert
            var items = Assert.IsType<List<Picture>>(pictures);
            Assert.True(items.Count > 0);
        }



        // Tester le code NotFound /api/pictures/0
        [Fact]
        public async Task GetById_UnknownGuidPassed_ReturnsNotFoundResultAsync()
        {
            // Préparation de l'appel à l'API
            HttpClient client = new HttpClient();

            // Act
            var content = await client.GetAsync("http://localhost:5010/" + "api/Pictures/0");
            var result = content.StatusCode;

            // Assert
            Assert.IsType<HttpStatusCode>(result);
            Assert.Equal(HttpStatusCode.NotFound, result);

        }

        // Tester le code OK /api/pictures/2
        [Fact]
        public async Task GetById_ExistingGuidPassed_ReturnsOkResultAsync()
        {
            // Préparation de l'appel à l'API
            HttpClient client = new HttpClient();

            // Act
            var content = await client.GetAsync("http://localhost:5010/" + "api/Pictures/2");
            var result = content.StatusCode;

            // Assert
            Assert.IsType<HttpStatusCode>(result);
            Assert.Equal(HttpStatusCode.OK, result);
        }

        // Tester si on reçoit le bon objet /api/pictures/2
        [Fact]
        public async Task GetById_ExistingGuidPassed_ReturnsRightItemAsync()
        {
            int testGuid = 2;

            // Préparation de l'appel à l'API
            HttpClient client = new HttpClient();

            // Act
            Stream streamTask = await client.GetStreamAsync("http://localhost:5010/" + "api/Pictures/2");
            Picture picture = await JsonSerializer.DeserializeAsync<Picture>(streamTask,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );

            // Assert
            Assert.IsType<Picture>(picture);
            Assert.Equal(testGuid, picture.PictureId);
        }
    }
}
