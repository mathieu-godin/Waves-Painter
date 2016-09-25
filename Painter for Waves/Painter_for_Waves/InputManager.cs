/*
InputManager.cs
---------------

Par Mathieu Godin

Rôle : Composant qui s'occupe de la gestion
       des intrants au niveau des différents
       périphériques connectés à l'appareil
       utilisé

Créé : 12 septembre 2016
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Painter_for_Waves
{
    /// <summary>
    /// Composant qui s'occupe de la gestion des intrants
    /// </summary>
    public class InputManager : Microsoft.Xna.Framework.GameComponent
    {
        Keys[] AnciennesTouches { get; set; }
        Keys[] NouvellesTouches { get; set; }
        MouseState AncienÉtatSouris { get; set; }
        MouseState NouvelÉtatSouris { get; set; }
        KeyboardState ÉtatClavier { get; set; }

        /// <summary>
        /// Vrai si au moins une touche du clavier est enfoncée
        /// </summary>
        public bool EstClavierActivé
        {
            get
            {
                return NouvellesTouches.Length > 0;
            }
        }

        /// <summary>
        /// Vrai si la souris est visible à l'écran
        /// </summary>
        public bool EstSourisActive
        {
            get
            {
                return Game.IsMouseVisible;
            }
        }

        /// <summary>
        /// Constructeur qui ne fait qu'appeler celui du parent
        /// </summary>
        /// <param name="game">Jeu de type Game</param>
        public InputManager(Game game) : base(game) { }

        /// <summary>
        /// Initialise les propriétés de l'objet
        /// </summary>
        public override void Initialize()
        {
            AnciennesTouches = new Keys[0];
            NouvellesTouches = new Keys[0];
        }

        /// <summary>
        /// Met à jour l'état des différents périphériques
        /// </summary>
        /// <param name="gameTime">Informations sur le temps de jeu de type GameTime</param>
        public override void Update(GameTime gameTime)
        {
            AnciennesTouches = NouvellesTouches;
            ÉtatClavier = Keyboard.GetState();
            NouvellesTouches = ÉtatClavier.GetPressedKeys();
            AncienÉtatSouris = NouvelÉtatSouris;
            NouvelÉtatSouris = Mouse.GetState();
        }

        /// <summary>
        /// Vrai si la touche de l'énumération Keys envoyée en paramètre est enfoncée
        /// </summary>
        /// <param name="touche">Touche de l'énumération Keys</param>
        /// <returns>Booléen</returns>
        public bool EstEnfoncée(Keys touche)
        {
            return ÉtatClavier.IsKeyDown(touche);
        }

        /// <summary>
        /// Vrai si le bouton droit de la souris est enfoncé mais qu'il l'était déjà
        /// </summary>
        /// <returns>Booléen</returns>
        public bool EstAncienClicDroit()
        {
            return AncienÉtatSouris.RightButton == ButtonState.Pressed && NouvelÉtatSouris.RightButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Vrai si le bouton gauche de la souris est enfoncé mais qu'il l'était déjà
        /// </summary>
        /// <returns>Booléen</returns>
        public bool EstAncienClicGauche()
        {
            return AncienÉtatSouris.LeftButton == ButtonState.Pressed && NouvelÉtatSouris.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Vrai si le bouton droit de la souris vient tout juste d'être enfoncé
        /// </summary>
        /// <returns>Booléen</returns>
        public bool EstNouveauClicDroit()
        {
            return AncienÉtatSouris.RightButton == ButtonState.Released && NouvelÉtatSouris.RightButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Vrai si le bouton gauche de la souris vient tout juste d'être enfoncé
        /// </summary>
        /// <returns>Booléen</returns>
        public bool EstNouveauClicGauche()
        {
            return AncienÉtatSouris.LeftButton == ButtonState.Released && NouvelÉtatSouris.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Retourne la position actuelle de la souris dans le fenêtre du jeu
        /// </summary>
        /// <returns>Point</returns>
        public Point GetPositionSouris()
        {
            return new Point(NouvelÉtatSouris.X, NouvelÉtatSouris.Y);
        }

        /// <summary>
        /// Vrai si la touche envoyée en paramètre vient tout juste d'être enfoncée
        /// </summary>
        /// <param name="touche">Touche de l'énumération Keys</param>
        /// <returns>Booléen</returns>
        public bool EstNouvelleTouche(Keys touche)
        {
            int NbTouches = AnciennesTouches.Length;
            bool EstNouvelleTouche = ÉtatClavier.IsKeyDown(touche);
            int i = 0;

            while (i < NbTouches && EstNouvelleTouche)
            {
                EstNouvelleTouche = AnciennesTouches[i] != touche;
                ++i;
            }

            return EstNouvelleTouche;
        }
    }
}