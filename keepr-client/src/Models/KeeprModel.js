export class Keepr {
  constructor(data) {
    this.id = data.id || data._id
    this.name = data.name
    this.description = data.description
    this.img = data.img
    this.shares = data.shares
    this.views = data.views
    this.keeps = data.keeps
    this.creatorId = data.creatorId
  }
}
