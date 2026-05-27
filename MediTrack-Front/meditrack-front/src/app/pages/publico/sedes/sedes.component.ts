import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { SedesService, Sede } from '../../../core/services/sedes.service';

@Component({
  selector: 'app-sedes',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './sedes.component.html',
  styleUrl: './sedes.component.scss'
})
export class SedesComponent implements OnInit {
  sedes: Sede[] = [];

  constructor(private sedesService: SedesService) {}

  ngOnInit(): void {
    this.sedesService.obtenerTodas().subscribe(data => this.sedes = data);
  }
}