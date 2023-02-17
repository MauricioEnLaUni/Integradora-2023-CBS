class Location {
  constructor(
    state: string,
    address?: string,
    street?: string,
    colony?: string,
    postal_code?: string,
    city?: string,
    country?: string,
    latitude?: string,
    longitude?: string
  ) { }
}

class Provider {
  constructor(
    readonly id: string,
    readonly marca: string,
    readonly provider_name: string
  ) {
  }
}

export class Material {

  constructor(
    readonly code: string,
    readonly model: string,
    private quantity: number,
    readonly dateAcquired: Date,
    private state: number,
    readonly location: Location,
    readonly priceAcquired: number,
    private currentPrice: number,
    readonly provider?: Provider,
  ) { }
}