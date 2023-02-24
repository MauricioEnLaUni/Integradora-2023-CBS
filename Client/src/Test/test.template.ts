// Set environment variable to test
process.env.NODE_ENV = 'test';

import mongoose from "mongoose";
import { Person } from "../Models/Person";

import chai from "chai";
import chaiHttp from 'chai-http';

const server = 'https://localhost:7135';
const should = chai.should;

chai.use(chaiHttp);
describe('Person', () => {
  beforeEach((done) => {
    done();
  });

  describe('/GET people', () => {
    it('it should get every person', (done) => {
      chai.request(server)
        .get('/users')
        .end((err, res) => {
          res.should.have.status(200);
          res.body.should.be.a('array');
          res.body.length.should.be.eql(0);
        done();
        });
    });
  });
});