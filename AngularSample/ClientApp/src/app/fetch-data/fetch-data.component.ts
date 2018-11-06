import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  private _httpClient: HttpClient;
  private _baseUrl: string;

  public forecasts: WeatherForecast[];

  public buttons: CustomButton[];

  public lookupColorName(color: string, id: string): void {    
    this._httpClient.post<ColorName>(this._baseUrl + 'api/SampleData/LookupColorName', color).subscribe(result => {
      this.buttons.filter(function (entry) { return entry.id === id; })[0].result = result.colorName;
    }, error => console.error(error));
  };

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<WeatherForecast[]>(baseUrl + 'api/SampleData/WeatherForecasts').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));

    http.get<CustomButton[]>(baseUrl + 'api/SampleData/CustomButtons').subscribe(result => {
      this.buttons = result;
    }, error => console.error(error));

    this._httpClient = http;
    this._baseUrl = baseUrl;
  }
}

interface WeatherForecast {
  dateFormatted: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

interface CustomButton {
  id: string;
  color: string;
  text: string;
  result: string;
}

interface ColorName {
   colorName : string;
}
