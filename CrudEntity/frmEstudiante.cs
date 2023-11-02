using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrudEntity
{
    public partial class frmEstudiante : Form
    {
        public frmEstudiante()
        {
            InitializeComponent();
        }

        EstudianteMCN metodosEstudiante= new EstudianteMCN();

        private void Imprimir()
        {
            dgvEstudiantes.DataSource = null;
            dgvEstudiantes.DataSource = metodosEstudiante.ObtenerTodos();
        }

        private void frmEstudiante_Load(object sender, EventArgs e)
        {
            Imprimir();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            EstudianteDto est = metodosEstudiante.BuscarPorCarnet(int.Parse(txtCarnet.Text));

            if (est is null)
            {
                MessageBox.Show($"No se ha encontrado al estudiante con carnet: {txtCarnet.Text}");
                return;
            }

            txtNombres.Text = est.Nombres;
            txtApellidos.Text = est.Apellidos;
            dtpFecha.Value = est.FechaNac ?? DateTime.Now;
            txtNota.Text = est.Nota.ToString();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            EstudianteDto est = metodosEstudiante.BuscarPorCarnet(int.Parse(txtCarnet.Text));

            if (est != null)
            {
                MessageBox.Show($"Ya existe un estudiante con carnet: {txtCarnet.Text}");
                return;
            }

            est = new EstudianteDto()
            {
                Carnet = int.Parse(txtCarnet.Text),
                Nombres = txtNombres.Text,
                Apellidos = txtApellidos.Text,
                FechaNac = dtpFecha.Value,
                Nota = int.Parse(txtNota.Text)
            };

            bool resultado = metodosEstudiante.Insertar(est);

            if (resultado)
            {
                MessageBox.Show("Se ha agregado al estudiante exitosamente");
                Imprimir();
            }
            else
                MessageBox.Show("No se ha podido agregar al estudiante");
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            EstudianteDto est = metodosEstudiante.BuscarPorCarnet(int.Parse(txtCarnet.Text));

            if (est is null)
            {
                MessageBox.Show($"No se ha encontrado al estudiante con carnet: {txtCarnet.Text}");
                return;
            }

            est = new EstudianteDto()
            {
                Carnet = int.Parse(txtCarnet.Text),
                Nombres = txtNombres.Text,
                Apellidos = txtApellidos.Text,
                FechaNac = dtpFecha.Value,
                Nota = int.Parse(txtNota.Text)
            };

            bool resultado = metodosEstudiante.Actualizar(est);

            if (resultado)
            {
                MessageBox.Show("Se ha actualizado el estudiante exitosamente");
                Imprimir();
            }
            else
                MessageBox.Show("No se ha podido actualizar al estudiante");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int carnet = int.Parse(txtCarnet.Text);
            EstudianteDto est = metodosEstudiante.BuscarPorCarnet(carnet);

            if (est is null)
            {
                MessageBox.Show($"No se ha encontrado al estudiante con carnet: {carnet}");
                return;
            }

            bool resultado = metodosEstudiante.Eliminar(carnet);

            if (resultado)
            {
                MessageBox.Show("Se ha eliminado al estudiante exitosamente");
                Imprimir();
            }
            else
                MessageBox.Show("No se ha podido eliminar al estudiante");
        }
    }
}
