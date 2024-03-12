import React, { useEffect, useState } from 'react';


export function Employees() {
  const [content, setContent] = useState(<EmployeesList showForm={showForm}/>)

  function showList() {
    setContent(<EmployeesList showForm={showForm}/>)
  }

  function showForm(employee) {
    setContent(<EmployeesForm employee={employee} showList={showList}/>)
  }

  return (
    <div className='container my-5'>
      {content}
    </div>
  )
}


function EmployeesList(props) {
  const [employees, setEmployees] = useState([])

  function fetchEmployees() {
    fetch("http://localhost:8000/api/Employee")
      .then(response => {
        if (!response.ok) {
          throw new Error("Server response error...")
        }
        return response.json()
      })
      .then(data => {
        //console.log(data); //for debug only
        setEmployees(data)
      })
      .catch(error => {
        console.error(error)
      })
  }

  useEffect(() => fetchEmployees(), [])

  function deleteEmployee(id) {
    fetch(`http://localhost:8000/api/Employee?id=${id}`, {
      method: "DELETE"
    })
      .then(response => response.json())
      .then(data => fetchEmployees())
      .catch(error => console.error(error))
  }

  return (
    <>
      <h2 className="text-center mb-3">List of employees</h2>
      <button onClick={() => props.showForm({})} type='button' className='btn btn-primary me-2'>Add</button>
      <button onClick={() => fetchEmployees()} type='button' className='btn btn-outline-primary me-2'>Refresh content</button>

      <table className='table'>
        <thead>
          <tr>
            <th>Id</th>
            <th>Name</th>
            <th>Surname</th>
            <th>Patronymic</th>
            <th>Email</th>
            <th>Projects</th>
            <th>Objectives</th>
            <th>Action</th>
          </tr>
        </thead>

        <tbody>
          {
            employees.map((employee, index) => {
              return (
                <tr key={index}>
                  <td>{employee.id}</td>
                  <td>{employee.name}</td>
                  <td>{employee.surname}</td>
                  <td>{employee.patronymic}</td>
                  <td>{employee.email}</td>
                  <td>-</td>
                  <td>-</td>
                  <td style={{width: "10px", whiteSpace: "nowrap"}}>
                    <button onClick={() => props.showForm(employee)} type='button' className='btn btn-primary btn-sm me-2'>Edit</button>
                    <button onClick={() => deleteEmployee(employee.id)} type='button' className='btn btn-danger btn-sm'>Delete</button>
                  </td>
                </tr>
              )
            })
          }
        </tbody>
      </table>
    </>
  )
}

function EmployeesForm(props) {

  const [errorMessage, setErrorMessage] = useState("")

  function handleSubmit(event) {
    event.preventDefault()

    const formData = new FormData(event.target)
    const employee = Object.fromEntries(formData.entries())

    if (!employee.name || !employee.surname || !employee.patronymic || !employee.email) {
      setErrorMessage(
        <div className='alert alert-warning' role='alert'>
          Some data missing for employee creation
        </div>
      )
      return
    }

    if (props.employee.id) {
      fetch(`http://localhost:8000/api/Employee?id=${props.employee.id}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(employee)
    })
      .then(response => {
        if (!response.ok) {
          throw new Error("Server response error...")
        }
      })
      .then(data => props.showList())
      .catch(error => {
        console.error(error)
      })
    } else {
      fetch("http://localhost:8000/api/Employee", {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(employee)
      })
        .then(response => {
          if (!response.ok) {
            throw new Error("Server response error...")
          }
        })
        .then(data => props.showList())
        .catch(error => {
          console.error(error)
        })
    }
  }

  return (
    <>
      <h2 className="text-center mb-3">{props.employee.id ? "Edit employee" : "Add employee"}</h2>
      <div className='row'>

        <div className='col-lg-6 mx-auto'>

          {errorMessage}
          <form onSubmit={(event) => handleSubmit(event)}>
            {props.employee.id && <div className='rom mb-3'>
              <label className='col-sm-4 col-form-label'>Id</label>
              <div className='col-sm-8'>
                <input readOnly className='form-control-plaintext' name='name' 
                defaultValue={props.employee.id}/>
              </div>
            </div>}

            <div className='rom mb-3'>
              <label className='col-sm-4 col-form-label'>Name</label>
              <div className='col-sm-8'>
                <input className='form-control' name='name' 
                defaultValue={props.employee.name}/>
              </div>
            </div>

            <div className='rom mb-3'>
              <label className='col-sm-4 col-form-label'>Surname</label>
              <div className='col-sm-8'>
                <input className='form-control' name='surname' defaultValue={props.employee.surname}/>
              </div>
            </div>

            <div className='rom mb-3'>
              <label className='col-sm-4 col-form-label'>Patronymic</label>
              <div className='col-sm-8'>
                <input className='form-control' name='patronymic' defaultValue={props.employee.patronymic}/>
              </div>
            </div>

            <div className='rom mb-3'>
              <label className='col-sm-4 col-form-label'>Email</label>
              <div className='col-sm-8'>
                <input className='form-control' name='email' defaultValue={props.employee.email}/>
              </div>
            </div>

            <div className='row'>
              <div className='offset-sm-4 col-sm-4 d-grid'>
                <button type='submit' className='btn btn-primary btn-sm me-3'>
                  Save
                </button>
              </div>
              <div className='col-sm-4 d-grid'>
                <button onClick={() => props.showList()} type='button' className='btn btn-secondary me-2'>
                  Cancel
                </button>
              </div>
            </div>
          </form>
        </div>
      </div>
    </>
  )
}