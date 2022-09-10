import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-weather.component.html'
})
export class FetchWeatherComponent {
  public forecasts: WeatherForecast[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    const url = `${baseUrl}api/weatherforecast`;
    http.get<WeatherForecast[]>(url).subscribe({
      next: result => {
        this.forecasts = result;
      },
      error: err => console.error(err)
    }
  );
  }
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
