import React from 'react';
import 'react-bootstrap-table-next/dist/react-bootstrap-table2.min.css';
import BootstrapTable from 'react-bootstrap-table-next';

const SignupsTable = ({data}) => {

    const pad = (n, width, z) => {
        z = z || '0';
        n = n + '';
        return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
    };

    const ed = data.map(s => {
        const timeOfDayHour = pad(Math.round(s.timeOfDayMinutes / 60), 2);
        const timeOfDayMin = pad(s.timeOfDayMinutes % 60, 2);
        const date = s.prefferedStart.split('T')[0]; // new Date(s.prefferedStart).toDateSTring();
        return {
            'id': s.id,
            'firstName': s.firstName,
            'lastName': s.lastName,
            'timeOfDay': `${timeOfDayHour}:${timeOfDayMin}`,
            'email': s.email,
            'date': date,
            'activity': s.activityName,
            'comments': s.comments,
            'yearsExperience': s.yearsExperience
        }
    });

    const columns =[ 
        {
            dataField: 'activity',
            text: 'Activity',
            sort: true
        },
        {
            dataField: 'yearsExperience',
            text: "Exp (years)",
            style: { textAlign: 'center' },
            headerStyle: (colum, colIndex) => {
                return { width: '80px', textAlign: 'center' };
            },
            sort: false
        },
        {
            dataField: 'firstName',
            text: 'First Name',
            sort: true
        },
        {
            dataField: 'lastName',
            text: 'Last Name',
            sort: true
        },
        {
            dataField: 'email',
            text: 'Email',
            sort: true
        },
        {
            dataField: 'date',
            text: 'Start Date',
            sort: true
        },
        {
            dataField: 'timeOfDay',
            text: 'Time',
            sort: true,
            style: { textAlign: 'center' },
            headerStyle: (colum, colIndex) => {
                return { width: '80px', textAlign: 'center' };
            },
        },
 
    ];

    const expandRow = {
    renderer: row => {
        return (
           <div>
               <p>{row.firstName} {row.lastName} - <strong>{row.activity}</strong> ({row.yearsExperience} years exp)</p>
               <p><strong>Start:</strong> {row.date}</p>
               <p><strong>Time:</strong> {row.timeOfDay}</p>
               <p><strong>Email:</strong> {row.email}</p>
               <hr />
               <p><strong>Comments: </strong>{row.comments}</p>
            </div>
        )}
    };

    return (
        <div className="signupTableContainer">
            <p>Click on any row to reveal more information.</p>
            <BootstrapTable 
                bootstrap4
                keyField='id'
                expandRow={expandRow}
                data={ed}
                columns={columns} />
        </div>
    );
};

export default SignupsTable;
