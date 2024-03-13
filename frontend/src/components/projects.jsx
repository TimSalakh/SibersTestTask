import React, { useEffect, useState } from 'react';


export function Projects() {
  const [content, setContent] = useState(<ProjectsList showForm={showForm}/>)

  function showList() {
    setContent(<ProjectsList showForm={showForm}/>)
  }

  function showForm(project) {
    setContent(<ProjectsForm project={project} showList={showList}/>)
  }

  return (
    <div className='container my-5'>
      {content}
    </div>
  )
}


function ProjectsList(props) {
  const [projects, setProjects] = useState([])

  function fetchProjects() {
    fetch("http://localhost:8000/api/Project")
      .then(response => {
        if (!response.ok) {
          throw new Error("Server response error...")
        }
        return response.json()
      })
      .then(data => {
        setProjects(data)
      })
      .catch(error => {
        console.error(error)
      })
  }

  useEffect(() => fetchProjects(), [])

  function deleteProjects(id) {
    fetch(`http://localhost:8000/api/Project?id=${id}`, {
      method: "DELETE"
    })
      .then(response => response.json())
      .then(data => fetchProjects())
      .catch(error => console.error(error))
  }

  return (
    <>
      <h2 className="text-center mb-3">List of projects</h2>
      <button onClick={() => props.showForm({})} type='button' className='btn btn-primary me-2'>Add</button>
      <button onClick={() => fetchProjects()} type='button' className='btn btn-outline-primary me-2'>Refresh content</button>

      <table className='table'>
        <thead>
          <tr>
            <th>Name</th>
            <th>Customer</th>
            <th>Leader</th>
            <th>Executor</th>
            <th>Employees</th>
            <th>Objectives</th>
            <th>Start date</th>
            <th>End date</th>
            <th>Action</th>
          </tr>
        </thead>

        <tbody>
          {
            projects.map((project, index) => {
              return (
                <tr key={index}>
                  <td>{project.name}</td>
                  <td>{project.customer}</td>
                  <td>{project.leader && `${project.leader.name} ${project.leader.surname}`}</td>
                  <td>{project.executor}</td>
                  <td>
                    {project.employees && project.employees.map(e => {
                      return `${e.name} ${e.surname}`
                    }).join("; ")}
                  </td>
                  <td>
                    {project.objectives && project.objectives.map(o => o.name).join("; ")}
                  </td>
                  <td>{project.startDate}</td>
                  <td>{project.endDate}</td>
                  <td>{project.priority}</td>
                  
                  <td style={{width: "10px", whiteSpace: "nowrap"}}>
                    <button onClick={() => props.showForm(project)} type='button' className='btn btn-primary btn-sm me-2'>Edit</button>
                    <button onClick={() => deleteProjects(project.id)} type='button' className='btn btn-danger btn-sm'>Delete</button>
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

function ProjectsForm(props) {

  const [errorMessage, setErrorMessage] = useState("")

  function handleSubmit(event) {
    event.preventDefault()

    const formData = new FormData(event.target)
    const project = Object.fromEntries(formData.entries())

    if (!project.name || !project.customer || !project.executor || !project.leaderId || !project.startDate || !project.endDate || !project.priority) {
      setErrorMessage(
        <div className='alert alert-warning' role='alert'>
          Some data missing for project creation
        </div>
      )
      return
    }

    if (props.project.id) {
      fetch(`http://localhost:8000/api/Project?id=${props.employee.id}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(project)
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
      fetch("http://localhost:8000/api/Project", {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(project)
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
      <h2 className="text-center mb-3">{props.project.id ? "Edit project" : "Add project"}</h2>
      <div className='row'>

        <div className='col-lg-6 mx-auto'>

          {errorMessage}
          <form onSubmit={(event) => handleSubmit(event)}>
            {props.project.id && <div className='rom mb-3'>
              <label className='col-sm-4 col-form-label'>Id</label>
              <div className='col-sm-8'>
                <input readOnly className='form-control-plaintext' name='name' 
                defaultValue={props.project.id}/>
              </div>
            </div>}

            <div className='rom mb-3'>
              <label className='col-sm-4 col-form-label'>Name</label>
              <div className='col-sm-8'>
                <input className='form-control' name='name' 
                defaultValue={props.project.name}/>
              </div>
            </div>

            <div className='rom mb-3'>
              <label className='col-sm-4 col-form-label'>Customer</label>
              <div className='col-sm-8'>
                <input className='form-control' name='customer' defaultValue={props.project.customer}/>
              </div>
            </div>

            <div className='rom mb-3'>
              <label className='col-sm-4 col-form-label'>Leader Id</label>
              <div className='col-sm-8'>
                <input className='form-control' name='leaderId' defaultValue={props.project.leaderId}/>
              </div>
            </div>

            <div className='rom mb-3'>
              <label className='col-sm-4 col-form-label'>Executor</label>
              <div className='col-sm-8'>
                <input className='form-control' name='executor' defaultValue={props.project.executor}/>
              </div>
            </div>

            <div className='rom mb-3'>
              <label className='col-sm-4 col-form-label'>Start date</label>
              <div className='col-sm-8'>
                <input className='form-control' name='startDate' defaultValue={props.project.startDate}/>
              </div>
            </div>

            <div className='rom mb-3'>
              <label className='col-sm-4 col-form-label'>End date</label>
              <div className='col-sm-8'>
                <input className='form-control' name='endDate' defaultValue={props.project.endDate}/>
              </div>
            </div>

            <div className='rom mb-3'>
              <label className='col-sm-4 col-form-label'>Priority</label>
              <div className='col-sm-8'>
                <input className='form-control' name='priority' defaultValue={props.project.priority}/>
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