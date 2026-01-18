import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PanelComponent } from '../../components/panel/panel.component';
import { LoggerService } from '../../service';

@Component({
  selector: 'app-reportes',
  standalone: true,
  imports: [CommonModule, PanelComponent],
  templateUrl: './reportes.page.html',
  styleUrls: ['./reportes.page.scss']
})
export class ReportesPage implements OnInit {
  private readonly logger = inject(LoggerService).getLogger('ReportesPage');

  ngOnInit() {
    this.logger.info('Página de reportes inicializada');
  }
}
