using System;

namespace board_game {
    class BoatNuclearShip : Boat{
        private String nom;
        public String Nom {get => nom;}
        public int[] x = new int[] {' ',' ',' ',' '};
        public int[] y = new int[] {' ',' ',' ',' '};
        public int size;
        public int Size {get => size; set => size = value;}
        public int[] X { 
            get { return x ;}
            set { x = value ;}
            }
        public int[] Y {
             get { return y ;}
             set { y = value ;}
            }

        public BoatNuclearShip(){
            this.size = 4;
            this.nom = "NuclearShip";
        }
        public BoatNuclearShip(int[] coordX, int[] coordY){
            this.x = coordX; 
            this.y = coordY;
            this.size = 4;
            this.nom = "NuclearShip";
        }
    }
}