import React from 'react';
import { Link } from 'react-router-dom';


export function Navbar() {
  return (
      <nav className="navbar navbar-expand-lg navbar-light bg-light p-3 mb-5">
        <Link className="navbar-brand" to="/">Company App</Link>
        <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navbarNav">
          <ul className="navbar-nav">
            <li className="nav-item active">
              <Link className="nav-link text-dark" to="/">
                Home
              </Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link text-dark" to="/employees">
                Employees
              </Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link text-dark" to="/projects">
                Projects
              </Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link text-dark" to="/objectives">
                Objectives
              </Link>
            </li>
          </ul>
        </div>
    </nav>
  )
}


export function Footer() {
  return (
    <footer>
      <div className="container p-3 mt-5 border-top">
        <small className="d-block text-muted text-center">
          Made by Salakhutdinov Timur
        </small>
      </div>
    </footer>
  )
}