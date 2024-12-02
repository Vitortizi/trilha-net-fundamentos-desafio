using System.Text.Json;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();
        private ControleDeDados controleDeDados = new();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        private void CarregarVeiculos()
        {
            veiculos = controleDeDados.ListarDados();
        }

        /// <summary>
        /// Adiciona um veículo ao estacionamento, verificando se a placa já está cadastrada.
        /// Caso a placa seja válida e não esteja duplicada, salva a lista atualizada no arquivo JSON.
        /// </summary>
        /// <remarks>
        /// A placa é verificada quanto à duplicidade e se não está vazia ou nula.
        /// </remarks>
        public void AdicionarVeiculo()
        {

            Console.WriteLine("Digite a placa do veículo para estacionar:\n");
            Console.WriteLine("O formato esperado é: 3 letras, 1 número, 1 letra, 2 números (ex.: ABC1D23)");
            string placa = Console.ReadLine();

            CarregarVeiculos();

            if (veiculos.Any(x => x.ToUpper() == placa.ToUpper()))
            {
                Console.WriteLine($"Veículo com placa {placa} Já está no estacionamento!");
                return;
            }

            if (!PlacaValida(placa))
            {
                Console.WriteLine($"A placa {placa} é inválida. Tente novamente.");
                return;
            }

            veiculos.Add(placa.ToUpper());
            controleDeDados.SalvarDados(veiculos);

            Console.WriteLine($"Veículo com placa {placa} adicionado com sucesso!");
        }

        /// <summary>
        /// Remove um veículo do estacionamento com base na placa informada.
        /// Calcula o valor total a ser pago com base no tempo de permanência.
        /// Atualiza a lista de veículos no arquivo JSON.
        /// </summary>
        /// <remarks>
        /// Caso a placa não seja encontrada, exibe uma mensagem de erro.
        /// </remarks>
        public void RemoverVeiculo()
        {
            CarregarVeiculos();

            if (veiculos.Count == 0)
            {
                Console.WriteLine("O estacionamento está vazio.");
                return;
            }

            Console.WriteLine("Digite a placa do veículo para remover:");
            string placa = Console.ReadLine();

            if (veiculos.Any(x => x.ToUpper() == placa.ToUpper()))
            {
                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");
                if (!int.TryParse(Console.ReadLine(), out int horas) || horas < 0)
                {
                    Console.WriteLine("Quantidade de horas inválida. Tente novamente.");
                    return;
                }

                decimal valorTotal = precoInicial + precoPorHora * horas;

                veiculos.Remove(placa.ToUpper());
                controleDeDados.SalvarDados(veiculos);

                Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: {valorTotal:C}");
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente.");
            }
        }

        /// <summary>
        /// Lista todos os veículos atualmente estacionados.
        /// Os veículos são recuperados a partir do arquivo JSON armazenado.
        /// </summary>
        /// <remarks>
        /// Caso não existam veículos estacionados, exibe uma mensagem informativa.
        /// </remarks>
        public void ListarVeiculos()
        {
            CarregarVeiculos();

            if (veiculos.Count > 0)
            {
                Console.WriteLine("Os veículos estacionados são:");
                foreach (var veiculo in veiculos)
                {
                    Console.WriteLine($"- {veiculo}");
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }

        /// <summary>
        /// Valida se a placa fornecida segue o formato padrão Mercosul.
        /// O formato esperado é: 3 letras, 1 número, 1 letra, 2 números (ex.: ABC1D23).
        /// </summary>
        /// <param name="placa">A placa do veículo a ser validada.</param>
        /// <returns>
        /// Retorna <c>true</c> se a placa for válida; caso contrário, retorna <c>false</c>.
        /// </returns>
        private bool PlacaValida(string placa)
        {
            if (placa.Length != 7) return false; // Verifica se a placa tem exatamente 7 caracteres

            // Valida o formato: 3 letras, 1 número, 1 letra, 2 números
            return System.Text.RegularExpressions.Regex.IsMatch(placa.ToUpper(), @"^[A-Z]{3}[0-9][A-Z][0-9]{2}$");
        }

    }
}
