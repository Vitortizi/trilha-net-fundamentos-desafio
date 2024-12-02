using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DesafioFundamentos.Models
{
    public class ControleDeDados
    {
        private string filePath = "data/estacionamento.json";

        /// <summary>
        /// Verifica se o diretório necessário para armazenar os dados existe.
        /// Caso não exista, o diretório é criado automaticamente.
        /// Também garante a criação do arquivo JSON inicial, se necessário.
        /// </summary>
        public void CriarDiretorioDeDados()
        {
            string directoryPath = "data";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            CriarArquivoDeDados();
        }

        /// <summary>
        /// Verifica se o arquivo JSON necessário para armazenar os dados existe.
        /// Caso não exista, cria o arquivo com um conteúdo inicial vazio representando uma lista vazia.
        /// </summary>
        public void CriarArquivoDeDados()
        {
            if (!File.Exists(filePath))
            {
                // Cria um arquivo JSON vazio com uma lista inicializada
                File.WriteAllText(filePath, "[]");
            }
        }

        /// <summary>
        /// Salva uma lista de veículos no arquivo JSON especificado.
        /// </summary>
        /// <param name="veiculos">A lista de veículos a ser salva no arquivo JSON.</param>
        public void SalvarDados(List<string> veiculos)
        {
            string jsonString = JsonSerializer.Serialize(veiculos);
            veiculos = JsonSerializer.Deserialize<List<string>>(jsonString) ?? new List<string>();

            jsonString = JsonSerializer.Serialize(veiculos);
            File.WriteAllText(filePath, jsonString);
        }

        /// <summary>
        /// Lê os dados do arquivo JSON e os retorna como uma lista de veículos.
        /// </summary>
        /// <returns>Uma lista de veículos armazenados no arquivo JSON. Retorna uma lista vazia caso o arquivo esteja vazio.</returns>
        public List<string> ListarDados()
        {
            // Lê o conteúdo do arquivo JSON
            string jsonString = File.ReadAllText(filePath);

            // Desserializa o JSON para a lista de veículos
            List<string> veiculos = JsonSerializer.Deserialize<List<string>>(jsonString) ?? new List<string>();

            return veiculos;
        }
    }
}