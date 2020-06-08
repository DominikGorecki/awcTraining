import { activitiesApi } from '../../services/api'
import {SuccessResponse,FailResponse} from '../../services/CallResponses'

export const GetActivities = async () => {
    try {
        var result = await activitiesApi().get('/activities');
        return SuccessResponse(result.data);
    }
    catch (e)
    {
        return FailResponse(e.response.data);
    }
}

export const PutSignup = async(input) => {
    try {
        var result = await activitiesApi().put("", input);
        return SuccessResponse(result.data);
    }
    catch (e)
    {
        return FailResponse(e.response.data);
    }
}