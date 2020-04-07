﻿using System;
using System.Windows.Forms;

namespace CRMCore.Patterns
{
    abstract public partial class PatternEntityForm<TEntity> : PatternForm where TEntity : class, new()
    {
        public TEntity entity;
        public EntityFormMode mode;

        protected PatternEntityForm()
        {
            InitializeComponent();
        }

        //protected PatternEntityForm(TEntity entity, EntityFormMode mode)
        //{
        //    InitializeComponent();
        //    this.entity = entity;
        //    this.mode = mode;
        //}

        //public static implicit operator PatternEntityForm<TEntity>((TEntity entity, EntityFormMode mode) input)
        //{
        //    return new PatternEntityForm<TEntity>(input.entity, input.mode);
        //}

        /// <summary>
        /// Fills user controls with data from the param entity
        /// </summary>
        /// <param name="entity"></param>
        protected abstract void UnpackEntity();

        /// <summary>
        /// Checks input data from user controls and creates new entity from it
        /// </summary>
        /// <returns></returns>
        protected abstract bool PackEntity();

        /// <summary>
        /// Cansels packing of new entity.
        /// </summary>
        /// <param name="reason"> A messege which contains the reason of canseling </param>
        /// <returns></returns>
        protected virtual bool StopPackingEntity(string reason)
        {
            MessageBox.Show(reason, "Wrong input", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (PackEntity())
            {
                forceClosing = true;
                Close();
            }
        }

        private void PatternEntityForm_Load(object sender, EventArgs e)
        {
            Text = mode.ToString() + typeof(TEntity).Name.ToLower();
            switch (mode)
            {
                case EntityFormMode.Edit:
                    UnpackEntity();
                    break;
                case EntityFormMode.Observe:
                    buttonConfirm.Visible = false;
                    break;
            }
        }
    }
}
