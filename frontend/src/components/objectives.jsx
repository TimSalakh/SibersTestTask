import React, { useEffect, useState } from 'react';


export function Objectives() {
  const [content, setContent] = useState(<ObjectivesList showForm={showForm}/>)

  function showList() {
    setContent(<ObjectivesList showForm={showForm}/>)
  }

  function showForm(objective) {
    setContent(<ObjectivesForm objective={objective} showList={showList}/>)
  }

  return (
    <div className='container my-5'>
      {content}
    </div>
  )
}


function ObjectivesList(props) {
  const [objective, setObjectives] = useState([])

  function fetchObjectives() {
    fetch("http://localhost:8000/api/Objective")
      .then(response => {
        if (!response.ok) {
          throw new Error("Server response error...")
        }
        return response.json()
      })
      .then(data => {
        setObjectives(data)
      })
      .catch(error => {
        console.error(error)
      })
  }

  useEffect(() => fetchObjectives(), [])

  function deleteObjectives(id) {
    fetch(`http://localhost:8000/api/Objective?id=${id}`, {
      method: "DELETE"
    })
      .then(response => response.json())
      .then(data => fetchObjectives())
      .catch(error => console.error(error))
  }

  return (
    <>
      <h2 className="text-center mb-3">List of objectives</h2>
      <button onClick={() => props.showForm({})} type='button' className='btn btn-primary me-2'>Add</button>
      <button onClick={() => fetchObjectives()} type='button' className='btn btn-outline-primary me-2'>Refresh content</button>

      <table className='table'>
        <thead>
          <tr>
            <th>Name</th>
            <th>Executor</th>
            <th>Project</th>
            <th>Status</th>
            <th>Description</th>
            <th>Priority</th>
          </tr>
        </thead>

        <tbody>
          {
            objective.map((objective, index) => {
              return (
                <tr key={index}>
                  <td>{objective.name}</td>
                  <td>{objective.executor && `${objective.executor.name} ${objective.executor.surname}`}</td>
                  <td>{objective.project && objective.project.name}</td>
                  <td>{objective.status}</td>
                  <td>{objective.description}</td>
                  <td>{objective.priority}</td>
                  
                  <td style={{width: "10px", whiteSpace: "nowrap"}}>
                    <button onClick={() => props.showForm(objective)} type='button' className='btn btn-primary btn-sm me-2'>Edit</button>
                    <button onClick={() => deleteObjectives(objective.id)} type='button' className='btn btn-danger btn-sm'>Delete</button>
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

function ObjectivesForm(props) {

  const [errorMessage, setErrorMessage] = useState("")

  function handleSubmit(event) {
    event.preventDefault()

    const formData = new FormData(event.target)
    const objective = Object.fromEntries(formData.entries())

    if (!objective.name || !objective.executorId || !objective.projectId || !objective.status || !objective.description || !objective.priority) {
      setErrorMessage(
        <div className='alert alert-warning' role='alert'>
          Some data missing for objective creation
        </div>
      )
      return
    }

    if (props.objective.id) {
      fetch(`http://localhost:8000/api/Objective?id=${props.objective.id}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(objective)
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
      fetch("http://localhost:8000/api/Objective", {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(objective)
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
      <h2 className="text-center mb-3">{props.objective.id ? "Edit objective" : "Add objective"}</h2>
      <div className='row'>

        <div className='col-lg-6 mx-auto'>

          {errorMessage}
          <form onSubmit={(event) => handleSubmit(event)}>
            {props.objective.id && <div className='rom mb-3'>
              <label className='col-sm-4 col-form-label'>Id</label>
              <div className='col-sm-8'>
                <input readOnly className='form-control-plaintext' name='name' 
                defaultValue={props.objective.id}/>
              </div>
            </div>}

            <div className='rom mb-3'>
              <label className='col-sm-4 col-form-label'>Name</label>
              <div className='col-sm-8'>
                <input className='form-control' name='name' 
                defaultValue={props.objective.name}/>
              </div>
            </div>

            <div className='rom mb-3'>
              <label className='col-sm-4 col-form-label'>Executor id</label>
              <div className='col-sm-8'>
                <input className='form-control' name='executorId' defaultValue={props.objective.executorId}/>
              </div>
            </div>

            <div className='rom mb-3'>
              <label className='col-sm-4 col-form-label'>Project Id</label>
              <div className='col-sm-8'>
                <input className='form-control' name='projectId' defaultValue={props.objective.projectId}/>
              </div>
            </div>

            <div className='rom mb-3'>
              <label className='col-sm-4 col-form-label'>Status</label>
              <div className='col-sm-8'>
                <input className='form-control' name='status' defaultValue={props.objective.status}/>
              </div>
            </div>

            <div className='rom mb-3'>
              <label className='col-sm-4 col-form-label'>Description</label>
              <div className='col-sm-8'>
                <input className='form-control' name='description' defaultValue={props.objective.description}/>
              </div>
            </div>

            <div className='rom mb-3'>
              <label className='col-sm-4 col-form-label'>Priority</label>
              <div className='col-sm-8'>
                <input className='form-control' name='priority' defaultValue={props.objective.priority}/>
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