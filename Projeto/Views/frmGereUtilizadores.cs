﻿using iTasks.Controllers;
using iTasks.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTasks
{
    public partial class frmGereUtilizadores : Form
    {
        Utilizador utilizadorRecebido;
        public frmGereUtilizadores(Utilizador utilizadorRecebido)
        {
            InitializeComponent();
            this.utilizadorRecebido = utilizadorRecebido;

            if (utilizadorRecebido is Gestor gestor && gestor.gereUtilizadores == false)
            {
                txtNomeGestor.Enabled = false;
                txtPasswordGestor.Enabled = false;
                txtUsernameGestor.Enabled = false;
                cbDepartamento.Enabled = false;
                btGravarGestor.Enabled = false;
            }
            ReloadData();
        }
        // Eventos Guardar
        private void btGravarProg_Click(object sender, EventArgs e)
        {
            try
            {
                ProgramadorController.GravarProgramador(txtNomeProg.Text, txtUsernameProg.Text, txtPasswordProg.Text, (NivelExperiencia)cbNivelProg.SelectedItem, (Gestor)cbGestorProg.SelectedItem);
                MessageBox.Show("Programador gravado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReloadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ReloadData();
            }
        }
        private void btGravarGestor_Click(object sender, EventArgs e)
        {
            try
            {
                GestorController.GravarGestor(txtNomeGestor.Text, txtUsernameGestor.Text, txtPasswordGestor.Text, (Departamento)cbDepartamento.SelectedItem, chkGereUtilizadores.Checked);
                MessageBox.Show("Gestor gravado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ReloadData();
            }
        }
        // Eventos Editar
        private void btEditarProg_Click(object sender, EventArgs e)
        {
            try
            {
                Programador progSelecionado = (Programador)lstListaProgramadores.SelectedItem;
                NivelExperiencia nivelExperiencia = (NivelExperiencia)cbNivelProg.SelectedItem;

                if (progSelecionado == null)
                {
                    throw new Exception("Por favor, selecione um programador da lista.");
                }

                ProgramadorController.EditarProgramador(
                    progSelecionado,
                    txtNomeProg.Text,
                    txtUsernameProg.Text,
                    txtPasswordProg.Text,
                    nivelExperiencia,
                    (Gestor)cbGestorProg.SelectedItem
                    );

                lstListaProgramadores.DataSource = null;
                lstListaProgramadores.DataSource = ProgramadorController.ListarProgramadores();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ReloadData();
            }
        }
        private void btEditarGestor_Click(object sender, EventArgs e)
        {
            try
            {
                Gestor gestorSelecionado = (Gestor)lstListaGestores.SelectedItem;

                if (gestorSelecionado == null)
                    throw new Exception("Por favor, selecione um gestor da lista.");

                Departamento departamento = (Departamento)cbDepartamento.SelectedItem;

                GestorController.EditarGestor(
                    gestorSelecionado,
                    txtNomeGestor.Text,
                    txtUsernameGestor.Text,
                    txtPasswordGestor.Text,
                    departamento,
                    chkGereUtilizadores.Checked
                    );
                MessageBox.Show("Gestor editado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ReloadData();
            }
        }
        // Eventos de Eliminar
        private void btEliminarProg_Click(object sender, EventArgs e)
        {
            try
            {
                Programador progSelecionado = (Programador)lstListaProgramadores.SelectedItem;
                if (progSelecionado == null)
                    throw new Exception("Por favor, selecione um programador da lista.");

                ProgramadorController.EliminarProgramador(progSelecionado);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ReloadData();
            }
        }
        private void btEliminarGestor_Click(object sender, EventArgs e)
        {
            try
            {
                Gestor gestorSelecionado = (Gestor)lstListaGestores.SelectedItem;
                if (gestorSelecionado == null)
                    throw new Exception("Por favor, selecione um gestor da lista.");

                GestorController.EliminarGestor(gestorSelecionado, utilizadorRecebido);
                MessageBox.Show("Gestor eliminado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ReloadData();
            }
        }
        // Eventos de Atualização
        private void lstListaGestores_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Gestor gestorSelecionado = (Gestor)lstListaGestores.SelectedItem;

                if (gestorSelecionado == null)
                    return; // Se não houver gestor selecionado, não faz nada

                txtIdGestor.Text = gestorSelecionado.id.ToString();
                txtNomeGestor.Text = gestorSelecionado.nome;
                txtUsernameGestor.Text = gestorSelecionado.username;
                txtPasswordGestor.Text = gestorSelecionado.password;
                cbDepartamento.SelectedItem = gestorSelecionado.departamento;
                chkGereUtilizadores.Checked = gestorSelecionado.gereUtilizadores;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lstListaProgramadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Programador progSelecionado = (Programador)lstListaProgramadores.SelectedItem;

                if (progSelecionado == null)
                    return; // Se não houver programador selecionado, não faz nada

                txtIdProg.Text = progSelecionado.id.ToString();
                txtNomeProg.Text = progSelecionado.nome;
                txtUsernameProg.Text = progSelecionado.username;
                txtPasswordProg.Text = progSelecionado.password;
                cbNivelProg.SelectedItem = progSelecionado.nivelExperiencia;
                cbGestorProg.SelectedItem = progSelecionado.idGestor;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Funções Auxiliares
        private void ReloadData()
        {
            // Atualiza a combobox dos departamentos disponivies aos gestores
            cbDepartamento.DataSource = null;
            cbDepartamento.DataSource = Enum.GetValues(typeof(Departamento));
            // Atualiza a combobox dos niveis de experiencia disponiveis aos programadores
            cbNivelProg.DataSource = null;
            cbNivelProg.DataSource = Enum.GetValues(typeof(NivelExperiencia));
            // Atualiza a combobox dos gestores disponiveis aos programadores
            cbGestorProg.DataSource = null;
            cbGestorProg.DataSource = GestorController.ListarGestores();
            // Atualiza a lista de Gestores
            lstListaGestores.DataSource = null;
            lstListaGestores.DataSource = GestorController.ListarGestores();
            // Atualiza a lista de Programadores
            lstListaProgramadores.DataSource = null;
            lstListaProgramadores.DataSource = ProgramadorController.ListarProgramadores();
            // Atualiza os IDs dos gestores e programadores
            txtIdProg.Text = UserController.countId().ToString();
            txtIdGestor.Text = UserController.countId().ToString();

        }
    }
}
