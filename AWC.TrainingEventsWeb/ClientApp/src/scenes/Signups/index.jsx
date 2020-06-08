import React, {useEffect, useState} from 'react';
import FullPageLoading from '../../components/FullPageLoading';
import { GetSignups } from './actions'
import FullPageCard from '../../components/FullPageCard';
import ApiErrors from '../../components/ApiErrors';
import SignupsTable from './SignupsTable';

const Signups = () => {
    const [signups, setSignups] = useState(null);
    const [apiErrors, setApiErrors] = useState(null);

    useEffect(() => {
        (async() => {
            const response = await GetSignups();
            if(response.success)
                setSignups(response.value);
            else 
                setApiErrors(response.errors);
        })();
    }, []);

    if(apiErrors)
        return (
            <FullPageCard title={<h3>Errors</h3>}>
                <ApiErrors errors={apiErrors} />
            </FullPageCard>
        )

    if(signups)
        return (<SignupsTable data={signups} />)

    return (
        <FullPageLoading />
    );
};

export default Signups;
