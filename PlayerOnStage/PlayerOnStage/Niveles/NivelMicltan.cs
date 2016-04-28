using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PlayerOnStage
{
    class NivelMictlan : Nivel
    {
        Camara camera;
        Vector3 traslationTemp;
        //Para colocar una variable temporal y poder recuperarla cuando la camara se deje de mover
        private bool camaraEnMovimiento;
        float timeCounterPunta;
        public bool nivelCompleto = false;
        private int lado=1;
        List<PuntaPedernal> PuntaPedernal;
        List<Pua> ListaPua;
        private Mictlantecuhtli jefeMiclan;
        private PuntaPedernal punta;
        private Pua pua;
        private Pua pua2;
        private Pua pua3;
        private List<Cierra> listaCierras;
        private int TEMPLO = 12000;//13000
        float TIEMPO_CIERRA = 150;//100=1 segundo
        float TIEMPO_ZOMBIE = 30;//100=1 segundo

        float timeCounterCierra;
        float timeCounterZombie;

        float TIEMPO_PUNTA = 150;
        Random random;
        //Para sacar los elementos en pantalla despues de cierta tolerancia
        int TOLERANCIA = 100;


        public void setCamera(Camara camera)
        {
            this.camera = camera;
        }
        public Camara getCamera()
        {
            return this.camera;
        }
        public NivelMictlan(ContentManager content, GraphicsDevice device)
        {

            base.fondo = new int[,]{
                {3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3},
                {3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3},
                {3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3},
                {3, 3, 3, 3, 3, 3, 3, 3, 3, 3,  3, 3, 3, 3, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 3, 3, 3, 3, 3, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 12,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 12, 0, 0, 0, 0,  0, 0},
                {3, 0, 3, 3, 0, 0, 0, 3, 3, 0,  0, 3, 3, 3, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 3, 3, 3, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 12,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 12, 0, 0, 0, 0,  0, 0},
                {0, 3, 0, 0, 3, 3, 3, 0, 0, 3,  3, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  1, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 3, 3, 3, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 1, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 12,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 12, 0, 0, 0, 0,  0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  1, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 3, 3, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 1, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 12,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 12, 0, 0, 0, 0,  0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  1, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 3, 3, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 12, 0, 0, 0, 0,  0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  1, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 3, 3, 3, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 12, 0, 0, 0, 0,  0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 1, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 3, 3, 0, 0, 0, 0,  1, 1, 1, 1, 1, 1, 1, 0, 0, 0,  1, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 12, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 12, 0, 0, 0, 0,  0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 1, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 3, 0, 0, 0, 0, 0,  1, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 12, 12,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 12, 0, 0, 0, 0,  0, 0},
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  1, 0, 0, 0, 0, 0, 0, 0, 0, 0,  1, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  3, 3, 3, 3, 3, 3, 3, 3, 0, 0,  0, 3, 3, 3, 2, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 2,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 3, 3, 3, 0, 0, 0, 0, 3, 2,  2, 3, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0,12,12,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 12, 0, 0, 0,  0, 0},
                {1, 1, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  1, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  1, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 1, 1, 1, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 12,12,12,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 12, 0, 0, 0,  0, 0},
                {1, 1, 1, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 1, 0, 0, 0, 0, 0, 0, 1, 0,  1, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0,12,12, 12,12,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 12, 0, 2, 0,  0, 0},
                {1, 1, 1, 1, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 1, 1, 0, 0, 0,  0, 0, 1, 1, 1, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 1, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 1, 1, 1, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0,12,12,12,12, 12,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 12, 2, 2, 0,  0, 0},
                {1, 1, 1, 1, 1, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 1, 1, 1, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 1, 0, 0,  0, 0, 0, 0, 0, 1, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 3, 2, 3,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 2, 0, 0, 0, 0, 0,  3, 2, 0, 0, 0, 0, 2, 0, 0, 2,  0, 0, 2, 0, 0, 0, 0, 0, 3, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 3,  3, 0, 0, 0, 0, 0, 0,12, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0,0,  0, 0,0,0, 0, 12, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 3, 3,  2, 3, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0,12,12, 12,12,12,12,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 12, 0, 2,  0, 0},
                {1, 1, 1, 1, 1, 1, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 1, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  1, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 12,12, 0, 0,  0,0, 0, 0, 0, 0, 0, 0, 0,0,  0,0,0,0, 0, 12,12, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0,12,12, 12,12,12,12,12,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 12, 0, 2,  0, 0},
                {1, 1, 1, 1, 1, 1, 1, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 1, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 1, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  1, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 12,12,12, 0, 0,  0,0, 0, 0, 0, 0, 0, 0,0,0,  0,0,0,0, 0, 12,12,12,0,  0,  0, 0, 0,10, 11, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0,10,  11, 0,12,12, 12,12,12,12,12, 12,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0,0,  0,0,  0, 12,0,0, 2, 0,0},
                {1, 1, 1, 1, 1, 1, 1, 1, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 1,  0, 0, 0, 0, 0, 0, 0, 0, 1, 1,  1, 1, 0, 0, 0, 0, 1, 1, 0, 0,  0, 0, 0, 1, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 12, 12, 12,12, 0,  0,  0,0, 0,0, 0, 0, 0,0,0,0,  0,0,0,0, 12, 12,12,12,12, 0,  0, 0, 0, 5, 6, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 5,  6,12, 12,12,12,12,12,12, 12,12,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0,0, 0,0,  0, 12, 0,2, 2,0, 0},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1,  1, 1, 1, 1, 1, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 1, 1, 1, 1, 1, 1, 1, 1, 0,  0, 1, 1, 1, 1, 1, 1, 1, 1, 1,  1, 1, 1, 1, 1, 1, 1, 0, 0, 0,  1, 1, 1, 1, 1, 1, 1, 1, 1, 1,  1, 1, 1, 1, 1, 1, 1, 1, 1, 1,  1, 1, 1, 1, 1, 1, 1, 1, 1, 1,  1, 1, 1, 1, 1, 1, 1, 1, 1, 1,  1, 1, 1, 1, 1, 1, 1, 1, 1, 1,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0, 0, 0, 1, 1, 1, 1, 1, 1, 1,  1, 1, 1, 1, 1, 1, 1, 1, 1, 1,  1, 1, 1, 1, 1, 1, 1, 1, 1, 1,  1, 1, 1, 1, 1, 1, 1, 1, 1, 1,  1, 1, 1, 1, 1, 1, 1, 1, 1, 1,  1, 1, 1, 1, 1, 1, 1, 1, 1, 1,  1, 1},
            };
            /****************************************||||Zona de vacío||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||Pua filosas|||||||||||||||||||||||||Puntas Pedernal||||||----------------------------------------------Zombies-------------------------------------------------Puntas de Pedernal-----------------------------------------------------------------------------listaCierras---------------------------------------------------Agua----------------------------------Oz-------------------------------------------------------------Puntas------------------------------------templo de Miclantechutli-     Silla de miclantechutli  ** Estatua ----*/
            PuntaPedernal = new List<PuntaPedernal>();
            ListaPua = new List<Pua>();
            timeCounterPunta = TIEMPO_PUNTA;
            jefeMiclan = new Mictlantecuhtli();
            pua = new Pua();
            pua2 = new Pua();
            pua3 = new Pua();
            punta = new PuntaPedernal();
            base.Content = content;
            base.device = device;
            base.backgroundTexture = content.Load<Texture2D>("background1");
            base.cancionNivel = content.Load<Song>("Final");//Final
            Tiles.Content = Content;
            base.map = nivel.generar(fondo);

            //MediaPlayer.Play(base.cancionNivel);
            listaCierras = new List<Cierra>();
            random = new Random();
            timeCounterCierra = TIEMPO_CIERRA;
            timeCounterZombie = TIEMPO_ZOMBIE;

        }
        //Obligatorio
        public Mapeador getMapa()
        {
            return base.map;
        }

        public void Load(ContentManager Content, GraphicsDevice dev)
        {
            jefeMiclan.Load(Content, dev);
            punta.Load(Content);
            pua.Load(Content);
            pua2.Load(Content);
            pua3.Load(Content);
        }
        public void Update(GameTime gameTime, Player player)
        {
            /*
                        if (player.posicion.X >= 13000) { 
                            nivelCompleto = true;
                            player.posicion.X = 180;
                            player.posicion.Y = 1025; 
            
                        }*/
            if (player.posicion.X > TEMPLO && !player.muerteBool)
            {
                listaCierras.Clear();
                PuntaPedernal.Clear();
                ListaPua.Clear();
                jefeMiclan.Update(gameTime, player);
                base.jefe = (Jefe)jefeMiclan;
                base.banderaJefe = true;
                player.Update(jefeMiclan);

                if (jefeMiclan.moverCamara)
                {
                    if (!camaraEnMovimiento)
                    {
                        camaraEnMovimiento = true;
                        traslationTemp = new Vector3(camera.Transform.Translation.X, camera.Transform.Translation.Y, camera.Transform.Translation.Z);
                    }
                    else
                    {
                        float valorY = random.Next(1,50);
                        float valorX = random.Next(1,70);
                       
                        switch(lado)
                        {
                            case 1:
                               valorY*=-1;
                                valorX*=-1;
                                lado=2;
                               break;
                            case 2:
                                valorY*=-1;
                                valorX*=-1;
                                lado=1;
                                break;
                        }
                        camera.transform = Matrix.CreateTranslation(new Vector3(traslationTemp.X+ valorX,
                                                                          traslationTemp.Y+valorY, traslationTemp.Z));
                    }

                }
                else
                {
                    if(jefeMiclan.parado && player.paralizadoBool)
                        camera.transform = Matrix.CreateTranslation(traslationTemp);
                }
                

            }
            else
            {
                if (player.posicion.X > 4000)
                {
                    if (listaCierras.Count < 10)
                    {
                        int casoPosicionCierra = random.Next(1, 3); //1 es para que la cierra vaya hacia la izquierda y 2 es para que vaya a la derecha
                        bool movIzq = false;
                        int posicionPersonaje = (int)player.posicion.X;


                        switch (casoPosicionCierra)
                        {
                            case 1:
                                movIzq = true;
                                break;
                            case 2:
                                movIzq = false;
                                break;
                            default:
                                movIzq = false;
                                break;
                        }




                        if (timeCounterCierra-- == TIEMPO_CIERRA)
                        {
                            int velociadCierra = random.Next(1, 20);
                            Cierra cierra = new Cierra();
                            cierra.Load(Content);
                            cierra.setPisicionInicial(player, movIzq);
                            cierra.velocidadPiso = velociadCierra;
                            listaCierras.Add(cierra);
                        }
                        else if (timeCounterCierra <= 0)
                        {
                            timeCounterCierra = TIEMPO_CIERRA;
                        }




                    }
                    //Valida si hay listaCierras y si ya se saliron de pantalla
                    if (listaCierras.Count > 0)
                    {
                        for (int i = 0; i < listaCierras.Count; i++)
                        {
                            float toleranciaMayor = player.posicion.X + TOLERANCIA;
                            float toleranciaMenor = player.posicion.X - TOLERANCIA;
                            if (!(listaCierras[i].enemPosicion.X >= 4000))
                            {
                                listaCierras.Remove(listaCierras[i]);
                            }
                        }
                    }

                }


                if (player.posicion.X <= 1000 && ListaPua.Count == 0)
                {
                    Pua Pua = new Pua();
                    Pua.Load(Content);

                    ListaPua.Add(Pua);

                    Pua Pua2 = new Pua();
                    Pua2.Load(Content);
                    pua2.PuaPos.X = 5700;
                    pua2.PuaPos.Y = 1100;
                    ListaPua.Add(Pua2);

                    Pua Pua3 = new Pua();
                    Pua3.Load(Content);
                    pua3.PuaPos.X = 5600;
                    pua3.PuaPos.Y = 1100;
                    ListaPua.Add(Pua3);


                }
                else
                {
                    foreach (Pua pua in ListaPua)
                    {
                        pua.Update();
                        player.Update(pua);
                    }
                }

                // si es diferente de nulo quiere decir que ya llego con el jefe



                if (listaCierras.Count > 0)
                {
                    foreach (Cierra cierra in listaCierras)
                    {
                        cierra.Update(player);
                        player.Update(cierra);
                    }
                }

                //aqui se llama a la punta al juego
                if (player.posicion.X >= 1 && player.posicion.X < 12000)
                {
                    if (timeCounterPunta-- == TIEMPO_PUNTA)
                    {

                        PuntaPedernal punta = new PuntaPedernal();
                        punta.Load(Content);
                        Random random = new Random();
                        punta.PuntaPos.Y = random.Next(700, 1200);
                        PuntaPedernal.Add(punta);
                    }
                    else if (timeCounterPunta <= 0)
                    {
                        timeCounterPunta = TIEMPO_PUNTA;
                    }
                    foreach (PuntaPedernal punta in PuntaPedernal)
                    {

                        punta.Update(gameTime);
                        player.Update(punta);
                    }
                    if (PuntaPedernal.Count > 0)
                    {
                        for (int i = 0; i < PuntaPedernal.Count; i++)
                        {
                            if (PuntaPedernal[i].PuntaPos.X < player.posicion.X-600)
                            {
                                PuntaPedernal.Remove(PuntaPedernal[i]);
                            }
                        }
                    }
                }
                else
                {
                    PuntaPedernal.Clear();
                }

            }



        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Aqui  se le pone el fondo

            pua.Draw(gameTime, spriteBatch);
            pua2.Draw(gameTime, spriteBatch);
            pua3.Draw(gameTime, spriteBatch);


            foreach (Cierra cierra in listaCierras)
            {
                cierra.Draw(gameTime, spriteBatch);
            }
            foreach (PuntaPedernal punta in PuntaPedernal)
            {
                punta.Draw(gameTime, spriteBatch);
            }
            if (base.banderaJefe)
            {
                jefeMiclan.Draw(gameTime, spriteBatch);
            }
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0,
           device.DisplayMode.Width,
           device.DisplayMode.Height),
           Color.LightCyan);
            base.map.Draw(spriteBatch);
            device.Clear(Color.Black);




            //***********************
            //hay que poner una validacion para decir que este no se pinta if(...)

        }



    }
}
