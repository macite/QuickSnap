using System;
using System.Collections.Generic;
using SwinGameSDK;

namespace GameOfLife
{
	public class NeuralNetwork 
	{
		//Randomier 
		Random rnd = new Random ();

		//Fields
		List<InputNode> _inputs = new List<InputNode>();
		List<Node> _hidden = new List<Node>();
		List<OutputNode> _outputs = new List<OutputNode>();

		private Intelligence _parent;

		private int _generation;
		private float _fitnessScore = 0;
		private int _lifetime = 0;

		//Positioning 
		Point2D _position = new Point2D();

		//Properties

		public List<InputNode> Inputs
		{
			get { return _inputs; }
		}

		public List<Node> Hidden
		{
			get { return _hidden; }
		}

		public List<OutputNode> Outputs
		{
			get { return _outputs; }
		}

		public Point2D Position
		{
			get { return _position;}
			set { _position = value;}
		}

		public int Generation
		{
			get { return _generation; }
			set { _generation = value; }
		}

		public float Fitness
		{
			get { return _fitnessScore; }
		}

		public int Lifetime
		{
			get{ return _lifetime; }
			set{ _lifetime = value;}
		}

		public Intelligence Parent
		{
			get{ return _parent; }
			set{ _parent = value;}
		}


		//Contructors
		public NeuralNetwork (Intelligence ai, Grid grid, int[] boundries)
		{
			_parent = ai;

			int r1 = boundries[0];
			int r2 = boundries[1];
			int c1 = boundries[2];
			int c2 = boundries[3];

			for(int r=r1;r<r2;r++)
			{
				for(int c=c1;c<c2;c++)
				{
					InputNode input = new InputNode (grid [r, c]);
					OutputNode output = new OutputNode (grid [r, c]);
					_inputs.Add (input);
					_outputs.Add (output);
				}
			}

			for(int i=0;i<rnd.Next(_inputs.Count);i++)
			{
				Node NewNode = new Node (_inputs[rnd.Next(_inputs.Count-1)], _outputs[rnd.Next(_outputs.Count-1)]);
				_hidden.Add (NewNode);
			}
		}

		public NeuralNetwork (Intelligence ai, Grid grid, int[] boundries, NeuralNetwork male, NeuralNetwork female)
		{
			//Console.WriteLine ("male: "+ male.Hidden.Count+" female: "+female.Hidden.Count);
			_parent = ai;

			int r1 = boundries[0];
			int r2 = boundries[1];
			int c1 = boundries[2];
			int c2 = boundries[3];

			for(int r=r1;r<r2;r++)
			{
				for(int c=c1;c<c2;c++)
				{
					InputNode input = new InputNode (grid [r, c]);
					OutputNode output = new OutputNode (grid [r, c]);
					_inputs.Add (input);
					_outputs.Add (output);
				}
			}

			//Mixmatch the hidden from both based on random crossover
			//Genome with the less number of hidden nodes become the ressecive and other the dominent
			NeuralNetwork recessive = (Math.Min(male.Hidden.Count,female.Hidden.Count)==male.Hidden.Count)?male:female;
			NeuralNetwork dominant = (Math.Max(male.Hidden.Count,female.Hidden.Count)==male.Hidden.Count)?male:female;
			for(int c=0;c<recessive.Hidden.Count;c++)
			{
				//Biologically Crossover is random
				if (rnd.NextDouble ()> 0.5)
				{
					ExtractNodes (dominant.Hidden [c]);
				}
				else
				{
					ExtractNodes (recessive.Hidden [c]);
				}
			}
			for(int c=recessive.Hidden.Count;c<dominant.Hidden.Count;c++)
			{
				ExtractNodes (dominant.Hidden[c]);
			}

		}

		//Function

		private void ExtractNodes(Node h)
		{
			InputNode targetInput = null;
			InputNode input = h.Origin[0] as InputNode;
			if (input != null)
			{
				int targetCol = input.AssociatedCell.Col;
				int targetRow = input.AssociatedCell.Row;
				foreach(InputNode i in this.Inputs)
				{
					if (i.AssociatedCell.Col == targetCol &&
						i.AssociatedCell.Row == targetRow)
					{
						targetInput = i;
					}
				}
			}
			OutputNode targetOutput = null;
			OutputNode output = h.Destination[0] as OutputNode;
			if (output != null)
			{
				int targetCol = output.AssociatedCell.Col;
				int targetRow = output.AssociatedCell.Row;
				foreach(OutputNode o in this.Outputs)
				{
					if (o.AssociatedCell.Col == targetCol &&
						o.AssociatedCell.Row == targetRow)
					{
						targetOutput = o;
					}
				}
			}
			this._hidden.Add (new Node(targetInput,targetOutput));
		}

		public void Breed(NeuralNetwork partner)
		{
			foreach(Node node in partner.Hidden)
			{
				this._hidden.Add (node);	
			}
		}

		public void CalculateFitness()
		{
			if (_parent.Target.HasEnded)
			{
				_fitnessScore = _parent.Target.LiveCount + _lifetime;
				if (_parent.MaxFitness < _fitnessScore) _parent.MaxFitness = _fitnessScore;
			}
		}

		public void Process()
		{
			foreach(InputNode i in _inputs)
			{
				foreach(Node d in i.Destination)
				{
					Node dest = d.Follow ();
					while (dest!=null)
					{
						if (dest as OutputNode != null)
						{
							OutputNode output = dest as OutputNode;
							output.AssociatedCell.State = !output.AssociatedCell.State;
							break;
						}
						dest = dest.Follow ();
					}
				}
			}
		}

		private void PositionNodes(int x, int y)
		{
			for(int i=0; i<_inputs.Count; i++)
			{
				_inputs[i].X = x + 50;
				_inputs[i].Y = y + (12 * i);
			}

			for(int i=0; i<_hidden.Count; i++)
			{
				_hidden[i].X = x + 150;
				_hidden[i].Y = y + (12 * i);
			}

			for(int i=0; i<_outputs.Count; i++)
			{
				_outputs[i].X = x + 250;
				_outputs[i].Y = y + (12 * i);
			}
		}

		public void Draw(int x, int y)
		{
			PositionNodes (x,y);
			foreach (Node n in _hidden)
			{
				n.DrawConnections (SwinGame.RGBColor(153,153,255),SwinGame.RGBColor(153,153,255));
				n.DrawNode(SwinGame.RGBColor(153,153,255));
			}

			foreach (Node n in _inputs)
			{
				if(n.Destination.Count>0)
				n.DrawNode(SwinGame.RGBColor(255,153,153));
			}

			foreach (Node n in _outputs)
			{
				if(n.Origin.Count>0)
				n.DrawNode(SwinGame.RGBColor(153,255,153));
			}
		}
	}
}

