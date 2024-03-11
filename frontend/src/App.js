import axios from 'axios';
import React, { useState } from 'react';

function App() {
  const [employees, setEmployee] = useState(null);

  const getEmployeesUrl = 'http://localhost:8000/api/Employee';

  const fetchData = async () => {
    const response = await axios.get(getEmployeesUrl);
    setEmployee(response.data)
  }

  return (
    <div>
      <h1>Employees</h1>
      <h2>GET-method to display all employees</h2>

      <div>
        <button onClick={fetchData}>Fetch Employees</button>
      </div>

      <div>
      {employees &&
          employees.map((employee, index) => {
            return (
              <div key={index}>
                <div>
                  <p>{employee.id}</p>
                  <p>{employee.name}</p>
                  <p>{employee.surname}</p>
                  <p>{employee.patronymic}</p>
                  <p>{employee.email}</p>
                </div>
              </div>
            );
          })}
      </div>
    </div>
  );
}

export default App;
