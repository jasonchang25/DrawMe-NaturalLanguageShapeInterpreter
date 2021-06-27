import { Component, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { BreakpointObserver } from '@angular/cdk/layout';
import { MatSidenav } from '@angular/material/sidenav';
import { shapeInfo } from './_models/shapeInfo.model';
import { shapeInput } from './_models/shapeInput.model';
import { coordinate } from './_models/shapeInfo.model';
import { InputInterpreterService } from './_services/input-interpreter.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements AfterViewInit{
  @ViewChild(MatSidenav)
  sidenav!: MatSidenav;
  @ViewChild('myCanvas') myCanvas: any;
  private context: any;
  shapeInput = {} as shapeInput ;
  shapeInfo = {} as shapeInfo;
  isError = false;
  errorMessage = '';
  centerX: number = 0;
  centerY: number = 0;
  shapeType:String = '';

  constructor(private observer: BreakpointObserver, private inputInterpreterService: InputInterpreterService) {}

  ngAfterViewInit() {
    this.context = (this.myCanvas.nativeElement as HTMLCanvasElement).getContext('2d');
    this.centerX = (this.myCanvas.nativeElement as HTMLCanvasElement).width / 2;
    this.centerY = (this.myCanvas.nativeElement as HTMLCanvasElement).height / 2;
    this.observer.observe(['(max-width: 800px)']).subscribe((res) => {
      if (res.matches) {
        this.sidenav.mode = 'over';
        this.sidenav.close();
      } else {
        this.sidenav.mode = 'side';
        this.sidenav.open();
      }
    });
  }

  OnSubmit(): void {
    this.context.clearRect(0, 0, this.centerX * 2, this.centerY * 2);    
    this.inputInterpreterService.InterpretInput(this.shapeInput).subscribe(
      data => {
        this.isError = false;
        this.shapeInfo = data;
        this.shapeType = this.shapeInfo.shape;
        switch (this.shapeType){
          case "circle":{
            this.DrawCircle();
            break;
          }
          case "oval":{
            this.DrawOval();
            break;
          }
          default:{
            this.DrawShape();
            break;
          }
        }
      },
      err => {
        this.shapeType = 'Invalid Shape';
        this.DrawSadFace();
        this.errorMessage = err.error.message;
        this.isError = true;
      }
    );
  }

  public Example(input:string){
    switch(input){
      case 'isosceles triangle':
        this.shapeInput.input = 'draw an isosceles triangle with a width of 400 and a height of 200';
        break;
      case 'square':
        this.shapeInput.input = 'draw a square with a side length of 200';
        break;
      case 'scalene triangle':
        this.shapeInput.input = 'draw a scalene triangle with a lengtha of 300 and a lengthb of 240 and a lengthc of 180';
        break;
      case 'parallelogram':
        this.shapeInput.input = 'draw a parallelogram with a lengtha of 300 and a lengthb of 200';
        break;
        case 'equilateral triangle':
          this.shapeInput.input = 'draw an equilateral triangle with a length of 200';
          break;
      case 'pentagon':
        this.shapeInput.input = 'draw a pentagon with a side length of 150';
        break;
      case 'rectangle':
        this.shapeInput.input = 'draw a rectangle with a width of 300 and a height of 150';
        break;
      case 'hexagon':
        this.shapeInput.input = 'draw a hexagon with a side length of 150';
        break;
      case 'heptagon':
        this.shapeInput.input = 'draw a heptagon with a side length of 120';
        break;
      case 'octagon':
        this.shapeInput.input = 'draw an octagon with a side length of 120';
        break;
      case 'circle':
        this.shapeInput.input = 'draw a circle with a radius of 150';
        break;
      case 'oval':
        this.shapeInput.input = 'draw an oval with a width of 500 and a height of 300';
        break;
    }
    this.OnSubmit();
  }

  private DrawCircle(){
    var radius = this.shapeInfo.information['radius'];
    this.context.beginPath();
    this.context.arc(this.centerX,this.centerY,radius,0, 2*Math.PI);
    this.context.stroke();
  }

  private DrawOval(){
    var width = this.shapeInfo.information['width'];
    var height = this.shapeInfo.information['height'];
    this.context.beginPath();
    this.context.ellipse(this.centerX,this.centerY,width/2,height/2, 0, 0, 2 * Math.PI);
    this.context.stroke();
  }

  private DrawShape() {
    this.context.beginPath();
    this.shapeInfo.shapeVertices.forEach(coord => this.context.lineTo(this.centerX + coord.x, this.centerY + coord.y));
    this.context.stroke();
  }

  private DrawSadFace(){
    this.context.beginPath();
    this.context.arc(this.centerX, this.centerY, 150, 0, Math.PI * 2, true); // Outer circle
    this.context.moveTo(this.centerX + 90, this.centerY + 90);
    this.context.arc(this.centerX, this.centerY + 90, 90, 0, Math.PI, true);  // Mouth (anti-clockwise)
    this.context.moveTo(this.centerX - 30, this.centerY - 45);
    this.context.arc(this.centerX - 45, this.centerY - 45, 15, 0, Math.PI * 2, true);  // Left eye
    this.context.moveTo(this.centerX + 60, this.centerY - 45);
    this.context.arc(this.centerX + 45, this.centerY - 45, 15, 0, Math.PI * 2, true);  // Right eye
    this.context.stroke();
  }
}