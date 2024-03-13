import React from 'react';
import ReactDOM from 'react-dom/client';
import { Navbar, Footer } from './components/layout';
import { Home } from './components/home';
import { Employees } from './components/employees';
import { Projects } from './components/projects';
import { Objectives } from './components/objectives';
import { BrowserRouter, Route, Routes } from 'react-router-dom'


function App() {
  return (
    <>
      <BrowserRouter>
        <Navbar />
        <Routes>
          <Route path='/' element={<Home />}/>
          <Route path='/employees' element={<Employees />}/>
          <Route path='/projects' element={<Projects />}/>
          <Route path='/objectives' element={<Objectives />}/>
        </Routes>
        <Footer />
      </BrowserRouter>
    </>
  )
}

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);
