using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using static System.Text.StringBuilder;
using static System.Random;

public class Rubik{
	public static void Main(String[] args){
		Cube c = new Cube(15);
		Console.WriteLine(c.ToString());
		foreach(int color in c.getShufflePath()){
			Console.Write(color);
			Console.Write(" ");
		}
		Console.WriteLine();
	}
}

class Cube{
	
	private Side front, back, left, right, top, bottom, holder;
	private List<int> shufflePath, solvePath;
	
	//Color Map Reference
	//Black  : Blank  : 0
	//Green  : Front  : 1
	//Blue   : Back   : 2
	//Orange : Left   : 3
	//Red    : Right  : 4
	//White  : Top    : 5
	//Yellow : Bottom : 6
	
	//Shuffle/Solve Map Reference
	//rotateLeftClockwise 		 : Turn Left/Orange Clockwise 			: 0
	//rotateLeftCounterClockwise 	 : Turn Left/Orange Counter Clockwise 	: 1
	//rotateRightClockwise 		 : Turn Right/Red Clockwise 			: 2
	//rotateRightCounterClockwise  : Turn Right/Red Counter Clockwise 	: 3
	//rotateTopClockwise 			 : Turn Top/White Clockwise 			: 4
	//rotateTopCounterClockwise 	 : Turn Top/White Counter Clockwise 	: 5
	//rotateBottomClockwise 		 : Turn Bottom/Yellow Clockwise 		: 6
	//rotateBottomCounterClockwise : Turn Bottom/Yellow Counter Clockwise : 7
	//rotateFrontClockwise 		 : Turn Front/Green Clockwise 			: 8
	//rotateFrontCounterClockwise  : Turn Front/Green Counter Clockwise 	: 9
	//rotateBackClockwise 		 : Turn Back/Blue Clockwise 			: 10
	//rotateBackCounterClockwise   : Turn Back/Blue Counter Clockwise 	: 11
	
	
	/*
	* Creates a new Cube object and shuffles it <shuffleValue> times. 
	* Setting <shuffleValue> to 0 will create a solved Cube.
	* 
	* @param shuffleValue: Number of moves shuffle() will make
	*/
	public Cube(int shuffleValue){
		this.top = new Side(5);
		this.bottom = new Side(6);
		this.front = new Side(1);
		this.back = new Side(2);
		this.left = new Side(3);
		this.right = new Side(4);
		this.holder = new Side(0);
		this.shufflePath = new List<int>();
		this.solvePath = new List<int>();
		
		this.shuffle(shuffleValue);
		this.solvePath.Clear();
	}
	
	public Cube(Cube clone){
		this.top = new Side(5);
		this.bottom = new Side(6);
		this.front = new Side(1);
		this.back = new Side(2);
		this.left = new Side(3);
		this.right = new Side(4);
		this.holder = new Side(0);
		this.shufflePath = new List<int>();
		this.solvePath = new List<int>();
		
		for(int i = 0; i < 9; i++){
			this.top.getSquare(i).setColor(clone.top.getSquare(i).getColor());
			this.bottom.getSquare(i).setColor(clone.bottom.getSquare(i).getColor());
			this.front.getSquare(i).setColor(clone.front.getSquare(i).getColor());
			this.back.getSquare(i).setColor(clone.back.getSquare(i).getColor());
			this.left.getSquare(i).setColor(clone.left.getSquare(i).getColor());
			this.right.getSquare(i).setColor(clone.right.getSquare(i).getColor());
		}
		
		for(int j = 0; j < clone.getSolvePath().Count; j++){
			this.solvePath.Add(clone.getSolvePath()[j]);
		}
		
		for(int k = 0; k < clone.getShufflePath().Count; k++){
			this.shufflePath.Add(clone.getShufflePath()[k]);
		}
	}
	/*
	* Randomly shuffles the Cube object <sAmount> times.
	* Will only shuffle clockwise to avoid redundancy.
	* Adds the corresponding path number to the Cube's shufflePath list after each move.
	* Writes <sAmount> to console when finished.
	*
	* @ param sAmount: Number of random moves shuffle will make
	*/
	public void shuffle(int sAmount){
		Random randomNumber = new Random();
		int shuffleAmount = sAmount; 
		int shuffleValue;
		for(int i = 0; i < shuffleAmount; i++){
			shuffleValue = randomNumber.Next(12) % 6;
			Console.Write(shuffleValue + " ");
			switch(shuffleValue){
				case 0:
					this.rotateLeftClockwise();
					this.shufflePath.Add(0);
					break;
				case 1:
					this.rotateRightClockwise();
					this.shufflePath.Add(2);
					break;
				case 2:
					this.rotateTopClockwise();
					this.shufflePath.Add(4);
					break;
				case 3:
					this.rotateBottomClockwise();
					this.shufflePath.Add(6);
					break;
				case 4:
					this.rotateFrontClockwise();
					this.shufflePath.Add(8);
					break;
				case 5:
					this.rotateBackClockwise();
					this.shufflePath.Add(10);
					break;
			}
		}
		Console.WriteLine();
		Console.WriteLine("Cube shuffled " + sAmount + " times.");
	}
	
	/* 
	* Shuffles the left/orange Side of the Cube object in a clockwise direction.
	* Adds the corresponding path number to the Cube's solvePath list when finished.
	*/
	public void rotateLeftClockwise(){
		holder.getSquare(0).setColor(front.getSquare(0).getColor());
		holder.getSquare(3).setColor(front.getSquare(3).getColor());
		holder.getSquare(6).setColor(front.getSquare(6).getColor());
		
		front.getSquare(0).setColor(top.getSquare(0).getColor());
		front.getSquare(3).setColor(top.getSquare(3).getColor());
		front.getSquare(6).setColor(top.getSquare(6).getColor());
		
		top.getSquare(0).setColor(back.getSquare(8).getColor());
		top.getSquare(3).setColor(back.getSquare(5).getColor());
		top.getSquare(6).setColor(back.getSquare(2).getColor());
		
		back.getSquare(2).setColor(bottom.getSquare(6).getColor());
		back.getSquare(5).setColor(bottom.getSquare(3).getColor());
		back.getSquare(8).setColor(bottom.getSquare(0).getColor());
		
		bottom.getSquare(0).setColor(holder.getSquare(0).getColor());
		bottom.getSquare(3).setColor(holder.getSquare(3).getColor());
		bottom.getSquare(6).setColor(holder.getSquare(6).getColor());
		
		this.solvePath.Add(0);
	}
	
	/* 
	* Shuffles the left/orange Side of the Cube object in a counter clockwise direction.
	* Adds the corresponding path number to the Cube's solvePath list when finished.
	*/
	public void rotateLeftCounterClockwise() {
		holder.getSquare(0).setColor(front.getSquare(0).getColor());
		holder.getSquare(3).setColor(front.getSquare(3).getColor());
		holder.getSquare(6).setColor(front.getSquare(6).getColor());
		
		front.getSquare(0).setColor(bottom.getSquare(0).getColor());
		front.getSquare(3).setColor(bottom.getSquare(3).getColor());
		front.getSquare(6).setColor(bottom.getSquare(6).getColor());
		
		bottom.getSquare(0).setColor(back.getSquare(8).getColor());
		bottom.getSquare(3).setColor(back.getSquare(5).getColor());
		bottom.getSquare(6).setColor(back.getSquare(2).getColor());
		
		back.getSquare(2).setColor(top.getSquare(6).getColor());
		back.getSquare(5).setColor(top.getSquare(3).getColor());
		back.getSquare(8).setColor(top.getSquare(0).getColor());
		
		top.getSquare(0).setColor(holder.getSquare(0).getColor());
		top.getSquare(3).setColor(holder.getSquare(3).getColor());
		top.getSquare(6).setColor(holder.getSquare(6).getColor());
		
		this.solvePath.Add(1);
	}
	
	/* 
	* Shuffles the right/red Side of the Cube object in a clockwise direction.
	* Adds the corresponding path number to the Cube's solvePath list when finished.
	*/
	public void rotateRightClockwise() {
		holder.getSquare(2).setColor(front.getSquare(2).getColor());
		holder.getSquare(5).setColor(front.getSquare(5).getColor());
		holder.getSquare(8).setColor(front.getSquare(8).getColor());
		
		front.getSquare(2).setColor(bottom.getSquare(2).getColor());
		front.getSquare(5).setColor(bottom.getSquare(5).getColor());
		front.getSquare(8).setColor(bottom.getSquare(8).getColor());
		
		bottom.getSquare(2).setColor(back.getSquare(6).getColor());
		bottom.getSquare(5).setColor(back.getSquare(3).getColor());
		bottom.getSquare(8).setColor(back.getSquare(0).getColor());
		
		back.getSquare(0).setColor(top.getSquare(8).getColor());
		back.getSquare(3).setColor(top.getSquare(5).getColor());
		back.getSquare(6).setColor(top.getSquare(2).getColor());
		
		top.getSquare(2).setColor(holder.getSquare(2).getColor());
		top.getSquare(5).setColor(holder.getSquare(5).getColor());
		top.getSquare(8).setColor(holder.getSquare(8).getColor());

		this.solvePath.Add(2);
	}
	
	/* 
	* Shuffles the right/red Side of the Cube object in a counter clockwise direction.
	* Adds the corresponding path number to the Cube's solvePath list when finished.
	*/
	public void rotateRightCounterClockwise() {
		holder.getSquare(2).setColor(front.getSquare(2).getColor());
		holder.getSquare(5).setColor(front.getSquare(5).getColor());
		holder.getSquare(8).setColor(front.getSquare(8).getColor());
		
		front.getSquare(2).setColor(top.getSquare(2).getColor());
		front.getSquare(5).setColor(top.getSquare(5).getColor());
		front.getSquare(8).setColor(top.getSquare(8).getColor());
		
		top.getSquare(2).setColor(back.getSquare(6).getColor());
		top.getSquare(5).setColor(back.getSquare(3).getColor());
		top.getSquare(8).setColor(back.getSquare(0).getColor());
		
		back.getSquare(0).setColor(bottom.getSquare(8).getColor());
		back.getSquare(3).setColor(bottom.getSquare(5).getColor());
		back.getSquare(6).setColor(bottom.getSquare(2).getColor());
		
		bottom.getSquare(2).setColor(holder.getSquare(2).getColor());
		bottom.getSquare(5).setColor(holder.getSquare(5).getColor());
		bottom.getSquare(8).setColor(holder.getSquare(8).getColor());
		
		this.solvePath.Add(3);
	}
	
	/* 
	* Shuffles the top/white Side of the Cube object in a clockwise direction.
	* Adds the corresponding path number to the Cube's solvePath list when finished.
	*/
	public void rotateTopClockwise() {
		for(int i = 0; i < 3; i++) {
			holder.getSquare(i).setColor(front.getSquare(i).getColor());
			front.getSquare(i).setColor(right.getSquare(i).getColor());
			right.getSquare(i).setColor(back.getSquare(i).getColor());
			back.getSquare(i).setColor(left.getSquare(i).getColor());
			left.getSquare(i).setColor(holder.getSquare(i).getColor());
		}
		this.solvePath.Add(4);
	}
	
	/* 
	* Shuffles the top/white Side of the Cube object in a counter clockwise direction.
	* Adds the corresponding path number to the Cube's solvePath list when finished.
	*/
	public void rotateTopCounterClockwise() {
		for(int i = 0; i < 3; i++) {
			holder.getSquare(i).setColor(front.getSquare(i).getColor());
			front.getSquare(i).setColor(left.getSquare(i).getColor());
			left.getSquare(i).setColor(back.getSquare(i).getColor());
			back.getSquare(i).setColor(right.getSquare(i).getColor());
			right.getSquare(i).setColor(holder.getSquare(i).getColor());
		}
		this.solvePath.Add(5);
	}
	
	/* 
	* Shuffles the bottom/yellow Side of the Cube object in a clockwise direction.
	* Adds the corresponding path number to the Cube's solvePath list when finished.
	*/
	public void rotateBottomClockwise() {
		for(int i = 6; i < 9; i++) {
			holder.getSquare(i).setColor(front.getSquare(i).getColor());
			front.getSquare(i).setColor(left.getSquare(i).getColor());
			left.getSquare(i).setColor(back.getSquare(i).getColor());
			back.getSquare(i).setColor(right.getSquare(i).getColor());
			right.getSquare(i).setColor(holder.getSquare(i).getColor());
		}
		this.solvePath.Add(6);
	}
	
	/* 
	* Shuffles the bottom/yellow Side of the Cube object in a count clockwise direction.
	* Adds the corresponding path number to the Cube's solvePath list when finished.
	*/
	public void rotateBottomCounterClockwise() {
		for(int i = 6; i < 9; i++) {
			holder.getSquare(i).setColor(front.getSquare(i).getColor());
			front.getSquare(i).setColor(right.getSquare(i).getColor());
			right.getSquare(i).setColor(back.getSquare(i).getColor());
			back.getSquare(i).setColor(left.getSquare(i).getColor());
			left.getSquare(i).setColor(holder.getSquare(i).getColor());
		}
		this.solvePath.Add(7);
	}
	
	/* 
	* Shuffles the front/green Side of the Cube object in a clockwise direction.
	* Adds the corresponding path number to the Cube's solvePath list when finished.
	*/
	public void rotateFrontClockwise() {
		for(int i = 6; i < 9; i++) {
			holder.getSquare(i).setColor(top.getSquare(i).getColor());
		}
		
		top.getSquare(6).setColor(left.getSquare(8).getColor());
		top.getSquare(7).setColor(left.getSquare(5).getColor());
		top.getSquare(8).setColor(left.getSquare(2).getColor());
		
		left.getSquare(2).setColor(bottom.getSquare(0).getColor());
		left.getSquare(5).setColor(bottom.getSquare(1).getColor());
		left.getSquare(8).setColor(bottom.getSquare(2).getColor());
		
		bottom.getSquare(0).setColor(right.getSquare(6).getColor());
		bottom.getSquare(1).setColor(right.getSquare(3).getColor());
		bottom.getSquare(2).setColor(right.getSquare(0).getColor());
		
		right.getSquare(0).setColor(holder.getSquare(6).getColor());
		right.getSquare(3).setColor(holder.getSquare(7).getColor());
		right.getSquare(6).setColor(holder.getSquare(8).getColor());

		this.solvePath.Add(8);
	}
	
	/* 
	* Shuffles the front/green Side of the Cube object in a counter clockwise direction.
	* Adds the corresponding path number to the Cube's solvePath list when finished.
	*/
	public void rotateFrontCounterClockwise() {
		for(int i = 6; i < 9; i++) {
			holder.getSquare(i).setColor(top.getSquare(i).getColor());
		}
		
		top.getSquare(6).setColor(right.getSquare(0).getColor());
		top.getSquare(7).setColor(right.getSquare(3).getColor());
		top.getSquare(8).setColor(right.getSquare(6).getColor());
		
		right.getSquare(0).setColor(bottom.getSquare(2).getColor());
		right.getSquare(3).setColor(bottom.getSquare(1).getColor());
		right.getSquare(6).setColor(bottom.getSquare(0).getColor());
		
		bottom.getSquare(0).setColor(left.getSquare(2).getColor());
		bottom.getSquare(1).setColor(left.getSquare(5).getColor());
		bottom.getSquare(2).setColor(left.getSquare(8).getColor());
		
		left.getSquare(2).setColor(holder.getSquare(8).getColor());
		left.getSquare(5).setColor(holder.getSquare(7).getColor());
		left.getSquare(8).setColor(holder.getSquare(6).getColor());
		
		this.solvePath.Add(9);
	}
	
	/* 
	* Shuffles the back/blue Side of the Cube object in a clockwise direction.
	* Adds the corresponding path number to the Cube's solvePath list when finished.
	*/
	public void rotateBackClockwise() {
		for(int i = 0; i < 3; i++) {
			holder.getSquare(i).setColor(top.getSquare(i).getColor());
		}
		
		top.getSquare(0).setColor(right.getSquare(2).getColor());
		top.getSquare(1).setColor(right.getSquare(5).getColor());
		top.getSquare(2).setColor(right.getSquare(8).getColor());

		right.getSquare(2).setColor(bottom.getSquare(8).getColor());
		right.getSquare(5).setColor(bottom.getSquare(7).getColor());
		right.getSquare(8).setColor(bottom.getSquare(6).getColor());

		bottom.getSquare(6).setColor(left.getSquare(0).getColor());
		bottom.getSquare(7).setColor(left.getSquare(3).getColor());
		bottom.getSquare(8).setColor(left.getSquare(6).getColor());

		left.getSquare(0).setColor(holder.getSquare(2).getColor());
		left.getSquare(3).setColor(holder.getSquare(1).getColor());
		left.getSquare(6).setColor(holder.getSquare(0).getColor());
		
		this.solvePath.Add(10);
	}
	
	/* 
	* Shuffles the back/blue Side of the Cube object in a counter clockwise direction.
	* Adds the corresponding path number to the Cube's solvePath list when finished.
	*/
	public void rotateBackCounterClockwise() {
		for(int i = 0; i < 3; i++) {
			holder.getSquare(i).setColor(top.getSquare(i).getColor());
		}
		
		top.getSquare(0).setColor(left.getSquare(6).getColor());
		top.getSquare(1).setColor(left.getSquare(3).getColor());
		top.getSquare(2).setColor(left.getSquare(0).getColor());
		
		left.getSquare(0).setColor(bottom.getSquare(6).getColor());
		left.getSquare(3).setColor(bottom.getSquare(7).getColor());
		left.getSquare(6).setColor(bottom.getSquare(8).getColor());
		
		bottom.getSquare(6).setColor(right.getSquare(8).getColor());
		bottom.getSquare(7).setColor(right.getSquare(5).getColor());
		bottom.getSquare(8).setColor(right.getSquare(2).getColor());
		
		right.getSquare(2).setColor(holder.getSquare(0).getColor());
		right.getSquare(5).setColor(holder.getSquare(1).getColor());
		right.getSquare(8).setColor(holder.getSquare(2).getColor());
		
		this.solvePath.Add(11);
	}
	
	public Cube createLeftClockwise(){
		Cube clone = new Cube(this);
		
		clone.holder.getSquare(0).setColor(clone.front.getSquare(0).getColor());
		clone.holder.getSquare(3).setColor(clone.front.getSquare(3).getColor());
		clone.holder.getSquare(6).setColor(clone.front.getSquare(6).getColor());
		
		clone.front.getSquare(0).setColor(clone.top.getSquare(0).getColor());
		clone.front.getSquare(3).setColor(clone.top.getSquare(3).getColor());
		clone.front.getSquare(6).setColor(clone.top.getSquare(6).getColor());
		
		clone.top.getSquare(0).setColor(clone.back.getSquare(8).getColor());
		clone.top.getSquare(3).setColor(clone.back.getSquare(5).getColor());
		clone.top.getSquare(6).setColor(clone.back.getSquare(2).getColor());
		
		clone.back.getSquare(2).setColor(clone.bottom.getSquare(6).getColor());
		clone.back.getSquare(5).setColor(clone.bottom.getSquare(3).getColor());
		clone.back.getSquare(8).setColor(clone.bottom.getSquare(0).getColor());
		
		clone.bottom.getSquare(0).setColor(clone.holder.getSquare(0).getColor());
		clone.bottom.getSquare(3).setColor(clone.holder.getSquare(3).getColor());
		clone.bottom.getSquare(6).setColor(clone.holder.getSquare(6).getColor());
		
		clone.solvePath.Add(0);
		
		return clone;
	}
	
	/* 
	* Shuffles the left/orange Side of the Cube object in a counter clockwise direction.
	* Adds the corresponding path number to the Cube's solvePath list when finished.
	*/
	public Cube createLeftCounterClockwise() {
		Cube clone = new Cube(this);
		
		clone.holder.getSquare(0).setColor(clone.front.getSquare(0).getColor());
		clone.holder.getSquare(3).setColor(clone.front.getSquare(3).getColor());
		clone.holder.getSquare(6).setColor(clone.front.getSquare(6).getColor());
		
		clone.front.getSquare(0).setColor(clone.bottom.getSquare(0).getColor());
		clone.front.getSquare(3).setColor(clone.bottom.getSquare(3).getColor());
		clone.front.getSquare(6).setColor(clone.bottom.getSquare(6).getColor());
		
		clone.bottom.getSquare(0).setColor(clone.back.getSquare(8).getColor());
		clone.bottom.getSquare(3).setColor(clone.back.getSquare(5).getColor());
		clone.bottom.getSquare(6).setColor(clone.back.getSquare(2).getColor());
		
		clone.back.getSquare(2).setColor(clone.top.getSquare(6).getColor());
		clone.back.getSquare(5).setColor(clone.top.getSquare(3).getColor());
		clone.back.getSquare(8).setColor(clone.top.getSquare(0).getColor());
		
		clone.top.getSquare(0).setColor(clone.holder.getSquare(0).getColor());
		clone.top.getSquare(3).setColor(clone.holder.getSquare(3).getColor());
		clone.top.getSquare(6).setColor(clone.holder.getSquare(6).getColor());
		
		clone.solvePath.Add(1);
		
		return clone;
	}
	
	/* 
	* Shuffles the right/red Side of the Cube object in a clockwise direction.
	* Adds the corresponding path number to the Cube's solvePath list when finished.
	*/
	public Cube createRightClockwise() {
		Cube clone = new Cube(this);
		
		clone.holder.getSquare(2).setColor(clone.front.getSquare(2).getColor());
		clone.holder.getSquare(5).setColor(clone.front.getSquare(5).getColor());
		clone.holder.getSquare(8).setColor(clone.front.getSquare(8).getColor());
		
		clone.front.getSquare(2).setColor(clone.bottom.getSquare(2).getColor());
		clone.front.getSquare(5).setColor(clone.bottom.getSquare(5).getColor());
		clone.front.getSquare(8).setColor(clone.bottom.getSquare(8).getColor());
		
		clone.bottom.getSquare(2).setColor(clone.back.getSquare(6).getColor());
		clone.bottom.getSquare(5).setColor(clone.back.getSquare(3).getColor());
		clone.bottom.getSquare(8).setColor(clone.back.getSquare(0).getColor());
		
		clone.back.getSquare(0).setColor(clone.top.getSquare(8).getColor());
		clone.back.getSquare(3).setColor(clone.top.getSquare(5).getColor());
		clone.back.getSquare(6).setColor(clone.top.getSquare(2).getColor());
		
		clone.top.getSquare(2).setColor(clone.holder.getSquare(2).getColor());
		clone.top.getSquare(5).setColor(clone.holder.getSquare(5).getColor());
		clone.top.getSquare(8).setColor(clone.holder.getSquare(8).getColor());

		clone.solvePath.Add(2);
		
		return clone;
	}
	
	/* 
	* Shuffles the right/red Side of the Cube object in a counter clockwise direction.
	* Adds the corresponding path number to the Cube's solvePath list when finished.
	*/
	public Cube createRightCounterClockwise() {
		Cube clone = new Cube(this);
		
		clone.holder.getSquare(2).setColor(clone.front.getSquare(2).getColor());
		clone.holder.getSquare(5).setColor(clone.front.getSquare(5).getColor());
		clone.holder.getSquare(8).setColor(clone.front.getSquare(8).getColor());
		
		clone.front.getSquare(2).setColor(clone.top.getSquare(2).getColor());
		clone.front.getSquare(5).setColor(clone.top.getSquare(5).getColor());
		clone.front.getSquare(8).setColor(clone.top.getSquare(8).getColor());
		
		clone.top.getSquare(2).setColor(clone.back.getSquare(6).getColor());
		clone.top.getSquare(5).setColor(clone.back.getSquare(3).getColor());
		clone.top.getSquare(8).setColor(clone.back.getSquare(0).getColor());
		
		clone.back.getSquare(0).setColor(clone.bottom.getSquare(8).getColor());
		clone.back.getSquare(3).setColor(clone.bottom.getSquare(5).getColor());
		clone.back.getSquare(6).setColor(clone.bottom.getSquare(2).getColor());
		
		clone.bottom.getSquare(2).setColor(clone.holder.getSquare(2).getColor());
		clone.bottom.getSquare(5).setColor(clone.holder.getSquare(5).getColor());
		clone.bottom.getSquare(8).setColor(clone.holder.getSquare(8).getColor());
		
		clone.solvePath.Add(3);
		
		return clone;
	}
	
	/* 
	* Shuffles the top/white Side of the Cube object in a clockwise direction.
	* Adds the corresponding path number to the Cube's solvePath list when finished.
	*/
	public Cube createTopClockwise() {
		Cube clone = new Cube(this);
		
		for(int i = 0; i < 3; i++) {
			clone.holder.getSquare(i).setColor(clone.front.getSquare(i).getColor());
			clone.front.getSquare(i).setColor(clone.right.getSquare(i).getColor());
			clone.right.getSquare(i).setColor(clone.back.getSquare(i).getColor());
			clone.back.getSquare(i).setColor(clone.left.getSquare(i).getColor());
			clone.left.getSquare(i).setColor(clone.holder.getSquare(i).getColor());
		}
		clone.solvePath.Add(4);
		
		return clone;
	}
	
	/* 
	* Shuffles the top/white Side of the Cube object in a counter clockwise direction.
	* Adds the corresponding path number to the Cube's solvePath list when finished.
	*/
	public Cube createTopCounterClockwise() {
		Cube clone = new Cube(this);
		
		for(int i = 0; i < 3; i++) {
			clone.holder.getSquare(i).setColor(clone.front.getSquare(i).getColor());
			clone.front.getSquare(i).setColor(clone.left.getSquare(i).getColor());
			clone.left.getSquare(i).setColor(clone.back.getSquare(i).getColor());
			clone.back.getSquare(i).setColor(clone.right.getSquare(i).getColor());
			clone.right.getSquare(i).setColor(clone.holder.getSquare(i).getColor());
		}
		clone.solvePath.Add(5);
		
		return clone;
	}
	
	/* 
	* Shuffles the bottom/yellow Side of the Cube object in a clockwise direction.
	* Adds the corresponding path number to the Cube's solvePath list when finished.
	*/
	public Cube createBottomClockwise() {
		Cube clone = new Cube(this);
		
		for(int i = 6; i < 9; i++) {
			clone.holder.getSquare(i).setColor(clone.front.getSquare(i).getColor());
			clone.front.getSquare(i).setColor(clone.left.getSquare(i).getColor());
			clone.left.getSquare(i).setColor(clone.back.getSquare(i).getColor());
			clone.back.getSquare(i).setColor(clone.right.getSquare(i).getColor());
			clone.right.getSquare(i).setColor(clone.holder.getSquare(i).getColor());
		}
		clone.solvePath.Add(6);
		
		return clone;
	}
	
	/* 
	* Shuffles the bottom/yellow Side of the Cube object in a count clockwise direction.
	* Adds the corresponding path number to the Cube's solvePath list when finished.
	*/
	public Cube createBottomCounterClockwise() {
		Cube clone = new Cube(this);
		
		for(int i = 6; i < 9; i++) {
			clone.holder.getSquare(i).setColor(clone.front.getSquare(i).getColor());
			clone.front.getSquare(i).setColor(clone.right.getSquare(i).getColor());
			clone.right.getSquare(i).setColor(clone.back.getSquare(i).getColor());
			clone.back.getSquare(i).setColor(clone.left.getSquare(i).getColor());
			clone.left.getSquare(i).setColor(clone.holder.getSquare(i).getColor());
		}
		clone.solvePath.Add(7);
		
		return clone;
	}
	
	/* 
	* Shuffles the front/green Side of the Cube object in a clockwise direction.
	* Adds the corresponding path number to the Cube's solvePath list when finished.
	*/
	public Cube createFrontClockwise() {
		Cube clone = new Cube(this);
		
		for(int i = 6; i < 9; i++) {
			clone.holder.getSquare(i).setColor(clone.top.getSquare(i).getColor());
		}
		
		clone.top.getSquare(6).setColor(clone.left.getSquare(8).getColor());
		clone.top.getSquare(7).setColor(clone.left.getSquare(5).getColor());
		clone.top.getSquare(8).setColor(clone.left.getSquare(2).getColor());
		
		clone.left.getSquare(2).setColor(clone.bottom.getSquare(0).getColor());
		clone.left.getSquare(5).setColor(clone.bottom.getSquare(1).getColor());
		clone.left.getSquare(8).setColor(clone.bottom.getSquare(2).getColor());
		
		clone.bottom.getSquare(0).setColor(clone.right.getSquare(6).getColor());
		clone.bottom.getSquare(1).setColor(clone.right.getSquare(3).getColor());
		clone.bottom.getSquare(2).setColor(clone.right.getSquare(0).getColor());
		
		clone.right.getSquare(0).setColor(clone.holder.getSquare(6).getColor());
		clone.right.getSquare(3).setColor(clone.holder.getSquare(7).getColor());
		clone.right.getSquare(6).setColor(clone.holder.getSquare(8).getColor());

		clone.solvePath.Add(8);
		
		return clone;
	}
	
	/* 
	* Shuffles the front/green Side of the Cube object in a counter clockwise direction.
	* Adds the corresponding path number to the Cube's solvePath list when finished.
	*/
	public Cube createFrontCounterClockwise() {
		Cube clone = new Cube(this);
		
		for(int i = 6; i < 9; i++) {
			clone.holder.getSquare(i).setColor(clone.top.getSquare(i).getColor());
		}
		
		clone.top.getSquare(6).setColor(clone.right.getSquare(0).getColor());
		clone.top.getSquare(7).setColor(clone.right.getSquare(3).getColor());
		clone.top.getSquare(8).setColor(clone.right.getSquare(6).getColor());
		
		clone.right.getSquare(0).setColor(clone.bottom.getSquare(2).getColor());
		clone.right.getSquare(3).setColor(clone.bottom.getSquare(1).getColor());
		clone.right.getSquare(6).setColor(clone.bottom.getSquare(0).getColor());
		
		clone.bottom.getSquare(0).setColor(clone.left.getSquare(2).getColor());
		clone.bottom.getSquare(1).setColor(clone.left.getSquare(5).getColor());
		clone.bottom.getSquare(2).setColor(clone.left.getSquare(8).getColor());
		
		clone.left.getSquare(2).setColor(clone.holder.getSquare(8).getColor());
		clone.left.getSquare(5).setColor(clone.holder.getSquare(7).getColor());
		clone.left.getSquare(8).setColor(clone.holder.getSquare(6).getColor());
		
		clone.solvePath.Add(9);
		
		return clone;
	}
	
	/* 
	* Shuffles the back/blue Side of the Cube object in a clockwise direction.
	* Adds the corresponding path number to the Cube's solvePath list when finished.
	*/
	public Cube createBackClockwise() {
		Cube clone = new Cube(this);
		
		for(int i = 0; i < 3; i++) {
			clone.holder.getSquare(i).setColor(clone.top.getSquare(i).getColor());
		}
		
		clone.top.getSquare(0).setColor(clone.right.getSquare(2).getColor());
		clone.top.getSquare(1).setColor(clone.right.getSquare(5).getColor());
		clone.top.getSquare(2).setColor(clone.right.getSquare(8).getColor());

		clone.right.getSquare(2).setColor(clone.bottom.getSquare(8).getColor());
		clone.right.getSquare(5).setColor(clone.bottom.getSquare(7).getColor());
		clone.right.getSquare(8).setColor(clone.bottom.getSquare(6).getColor());

		clone.bottom.getSquare(6).setColor(clone.left.getSquare(0).getColor());
		clone.bottom.getSquare(7).setColor(clone.left.getSquare(3).getColor());
		clone.bottom.getSquare(8).setColor(clone.left.getSquare(6).getColor());

		clone.left.getSquare(0).setColor(clone.holder.getSquare(2).getColor());
		clone.left.getSquare(3).setColor(clone.holder.getSquare(1).getColor());
		clone.left.getSquare(6).setColor(clone.holder.getSquare(0).getColor());
		
		clone.solvePath.Add(10);
		
		return clone;
	}
	
	/* 
	* Shuffles the back/blue Side of the Cube object in a counter clockwise direction.
	* Adds the corresponding path number to the Cube's solvePath list when finished.
	*/
	public Cube createBackCounterClockwise() {
		Cube clone = new Cube(this);
		
		for(int i = 0; i < 3; i++) {
			clone.holder.getSquare(i).setColor(clone.top.getSquare(i).getColor());
		}
		
		clone.top.getSquare(0).setColor(clone.left.getSquare(6).getColor());
		clone.top.getSquare(1).setColor(clone.left.getSquare(3).getColor());
		clone.top.getSquare(2).setColor(clone.left.getSquare(0).getColor());
		
		clone.left.getSquare(0).setColor(clone.bottom.getSquare(6).getColor());
		clone.left.getSquare(3).setColor(clone.bottom.getSquare(7).getColor());
		clone.left.getSquare(6).setColor(clone.bottom.getSquare(8).getColor());
		
		clone.bottom.getSquare(6).setColor(clone.right.getSquare(8).getColor());
		clone.bottom.getSquare(7).setColor(clone.right.getSquare(5).getColor());
		clone.bottom.getSquare(8).setColor(clone.right.getSquare(2).getColor());
		
		clone.right.getSquare(2).setColor(clone.holder.getSquare(0).getColor());
		clone.right.getSquare(5).setColor(clone.holder.getSquare(1).getColor());
		clone.right.getSquare(8).setColor(clone.holder.getSquare(2).getColor());
		
		clone.solvePath.Add(11);
		
		return clone;
	}
	/*
	* Returns the Cube's shufflePath list.
	*/
	public List<int> getShufflePath(){
		return this.shufflePath;
	}
	
	/* 
	* Returns the Cube's solvePath list.
	*/
	public List<int> getSolvePath(){
		return this.solvePath;
	}
	
	/* 
	* Returns a String output of a Cube object, formatted as:
	* <Name of Side>: 
	* <Color Number> | <Color Number> | <Color Number>
	* ------------------------------------------------
	* <Color Number> | <Color Number> | <Color Number> 
	* ------------------------------------------------
	* <Color Number> | <Color Number> | <Color Number> 
	*/
	public override String ToString() {
		StringBuilder sb = new StringBuilder();
		
		sb.Append("Top: \n");
		sb.Append(top.ToString());
		sb.Append("\n");
		sb.Append("Left: \n");
		sb.Append(left.ToString());
		sb.Append("\n");
		sb.Append("Front: \n");
		sb.Append(front.ToString());
		sb.Append("\n");
		sb.Append("Right: \n");
		sb.Append(right.ToString());
		sb.Append("\n");
		sb.Append("Back: \n");
		sb.Append(back.ToString());
		sb.Append("\n");
		sb.Append("Bottom: \n");
		sb.Append(bottom.ToString());
		
		return sb.ToString();
	}
}

class Side{
	private Square[] face = new Square[9];
	
	/*
	* Creates a new Side object, setting the Square color to black.
	*/
	public Side(){
		for(int i = 0; i < face.Length; i++){
			face[i] = new Square();
		}
	}
	
	/*
	* Creates a new Side object, setting the Square color to the entered <colorNumber> value.
	* 
	* @param colorNumber: The color number of the Side's face
	*/
	public Side(int c){
		for(int i = 0; i < face.Length; i++){
			face[i] = new Square(c);
		}
	}
	
	/*
	* Returns a single Square of the Side, where <pos> corresponds to the postion of the squares:
	* 0 | 1 | 2
	* ---------
	* 3 | 4 | 5
	* ---------
	* 6 | 7 | 8
	*
	* @param pos: The position number of Square in the face array
	*/
	public Square getSquare(int pos){
		return this.face[pos];
	}
	
	/*
	* Returns a String output of a Side object, formatted as:
	* <Name of Side>: 
	* <Color Number> | <Color Number> | <Color Number>
	* ------------------------------------------------
	* <Color Number> | <Color Number> | <Color Number> 
	* ------------------------------------------------
	* <Color Number> | <Color Number> | <Color Number>
	*/
	public override String ToString(){
		StringBuilder sb = new StringBuilder();
		
		for(int i = 0; i < 3; i++){
			sb.Append(face[i].getColor());
			if(i != 2){
				sb.Append(" | ");
			}
		}
		sb.Append("\n");
		sb.Append("---------");
		sb.Append("\n");
		
		for(int i = 3; i < 6; i++){
			sb.Append(face[i].getColor());
			if(i != 5){
				sb.Append(" | ");
			}
		}
		sb.Append("\n");
		sb.Append("---------");
		sb.Append("\n");
		
		for(int i = 6; i < 9; i++){
			sb.Append(face[i].getColor());
			if(i != 8){
				sb.Append(" | ");
			}
		}
		sb.Append("\n");
		
		return sb.ToString();
	}
}

class Square{
	
	private int color;
	
	public Square(){
		this.color = 0;
	}
	public Square(int c){
		this.color = c;
	}
	public void setColor(int c){
		this.color = c;
	}
	public int getColor(){
		return this.color;
	}
}