using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cookie_Clicla
{
    public partial class Form1 : Form
    {
        // Variáveis globais do jogo
        int pontos = 0;
        int pontosPorClique = 1;

        // Variáveis para gravar a posição e o tamanho original onde o botão nasceu
        int larguraOriginalBotao;
        int alturaOriginalBotao;
        int esquerdaOriginalBotao;
        int topoOriginalBotao;

        public Form1()
        {
            InitializeComponent();
            label1.Text = "0000";

            // Salva as configurações de fábrica do seu botão central antes de ser comido
            larguraOriginalBotao = button1.Width;
            alturaOriginalBotao = button1.Height;
            esquerdaOriginalBotao = button1.Left;
            topoOriginalBotao = button1.Top;

            // Inicia a trilha sonora de distorção em segundo plano
            TocarTrilhaEstridenteAsync();
        }

        // --- TRILHA SONORA MÁXIMA DISTORÇÃO (ESTOURA FONE) ---
        private async void TocarTrilhaEstridenteAsync()
        {
            await Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        Console.Beep(120, 150);
                        Console.Beep(100, 150);
                        Console.Beep(150, 200);
                        Console.Beep(80, 250);
                        Console.Beep(220, 100);
                        Console.Beep(330, 100);
                        Console.Beep(440, 150);
                        Console.Beep(130, 500);
                        Task.Delay(300).Wait();
                    }
                }
                catch { /* Proteção contra fechamento do jogo */ }
            });
        }

        // 1. Botão Principal (Diminui de tamanho a cada mordida/clique)
        private void button1_Click_1(object sender, EventArgs e)
        {
            // Som curto de estalo no clique
            Task.Run(() => Console.Beep(150, 40));

            // EFEITO COMER: Diminui a largura e altura se for maior que 50 pixels
            if (button1.Width > 50 && button1.Height > 50)
            {
                button1.Width -= 15;
                button1.Height -= 15;
            }

            // Lógica de pontos com trava estrita em 6767
            if (pontos + pontosPorClique <= 6767)
            {
                pontos += pontosPorClique;
                label1.Text = pontos.ToString("D4");
            }
            else if (pontos < 6767)
            {
                pontos = 6767;
                label1.Text = pontos.ToString("D4");
            }
            else
            {
                label1.Text = "6767 (MÁXIMO)";
            }
        }

        // 2. Botão de Upgrade: +2 Pontos (Custa 10)
        private void btn2cliques_Click(object sender, EventArgs e)
        {
            if (pontos >= 10)
            {
                pontos -= 10;
                pontosPorClique += 2;
                label1.Text = pontos.ToString("D4");
            }
            else
            {
                MessageBox.Show("Pontos insuficientes para comprar +2 por clique!", "Aviso");
            }
        }

        // 3. Botão de Upgrade: +5 Pontos (Custa 100)
        private void btn5cliques_Click(object sender, EventArgs e)
        {
            if (pontos >= 100)
            {
                pontos -= 100;
                pontosPorClique += 5;
                label1.Text = pontos.ToString("D4");
            }
            else
            {
                MessageBox.Show("Pontos insuficientes para comprar +5 por clique!", "Aviso");
            }
        }

        // 4. Botão de Upgrade: +50 Pontos (Custa 350)
        private void btn50cliques_Click(object sender, EventArgs e)
        {
            if (pontos >= 350)
            {
                pontos -= 350;
                pontosPorClique += 50;
                label1.Text = pontos.ToString("D4");
            }
            else
            {
                MessageBox.Show("Pontos insuficientes para comprar +50 por clique!", "Aviso");
            }
        }

        // 5. Botão: Comprar Novo Cookie (Reinicia o botão central ao estado original)
        private void btnComprarCookie_Click(object sender, EventArgs e)
        {
            if (pontos >= 10)
            {
                pontos -= 10; // Desconta os 10 pontos
                label1.Text = pontos.ToString("D4"); // Updates scoreboard

                // REINICIALIZAÇÃO COMPLETA: Restaura tamanho e coordenadas originais
                button1.Width = larguraOriginalBotao;
                button1.Height = alturaOriginalBotao;
                button1.Left = esquerdaOriginalBotao;
                button1.Top = topoOriginalBotao;

                // Som nativo de confirmação
                Task.Run(() => Console.Beep(600, 150));
            }
            else
            {
                MessageBox.Show("Pontos insuficientes para comprar um novo Cookie!", "Aviso");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
    }
}
