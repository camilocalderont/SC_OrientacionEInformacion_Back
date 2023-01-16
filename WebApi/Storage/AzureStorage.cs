using Dominio.Models.AtencionesGrupales;
using System.Net;
using WebApi.Responses;
using Azure.Storage.Blobs;
using Dominio.Utilities;
using Dominio.Models;
using System.IO;

namespace WebApi.Storage
{
    public class AzureStorage
    {
        private readonly IConfiguration Configuration;

        public AzureStorage(IConfiguration configuration)
        {
            Configuration = configuration;
        }        
        public bool validarAnexo(IFormFile anexo, long tamano, string extension)
        {
            if(anexo == null)
                return true;

            string nombreArchivo = anexo.FileName;
            var archivoArray = nombreArchivo.Split(".");
            var extensionArchivo = archivoArray[archivoArray.Length - 1];

            return anexo.Length <= tamano && extensionArchivo == extension;
        }

        public async Task<ArchivoCarga> CargarArchivoStream(IFormFile anexo, string rutaRemota)
        {
            ArchivoCarga archivo;
            using (var ms = new MemoryStream())
            {

                anexo.CopyTo(ms);

                ms.Seek(0, SeekOrigin.Begin);

                archivo = new ArchivoCarga
                {
                    archivoStream = ms,
                    carpetaInicial = Constants.CONTAINER,
                    rutaRemota = rutaRemota,
                    esPublico = true,
                };
 

                archivo.rutaLocal = await cargarAzureStream(archivo);

                ms.Close();

            }
            return archivo;
        }



        public async Task<string> cargarAzureStream(ArchivoCarga archivoCarga)
        {
            try
            {
                DotNetEnv.Env.Load();
                // Retrieve the connection string for use with the application. 

                string connectionString  = Configuration.GetValue<string>("CONNECTION-BLOB-STORAGE");
               



                // Create a BlobServiceClient object 
                var blobServiceClient = new BlobServiceClient(connectionString);

                //Create a unique name for the container
                string containerName = archivoCarga.carpetaInicial.ToLower();

                // Create the container and return a container client object
                BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
                await container.CreateIfNotExistsAsync();
                if (archivoCarga.esPublico)
                {
                    container.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);
                }

                // Get a reference to a blob
                BlobClient blobClient = container.GetBlobClient(archivoCarga.rutaRemota);

                // Upload data from the local file
                await blobClient.UploadAsync(archivoCarga.archivoStream, true);

                return blobClient.Uri.ToString();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
          
        }
    }
}
